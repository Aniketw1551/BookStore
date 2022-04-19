use BookStoreDB

-------Address Type Table------

create Table AddressType
(
	TypeId int identity(1,1) not null primary key,
	TypeName varchar(255) not null
);

Insert into AddressType
values('Home'),('Office'),('Other');

select * from AddressType

------Addresses Table-------

create Table Addresses
(
	AddressId int identity(1,1) primary key,
	Address varchar(max) not null,
	City varchar(100) not null,
	State varchar(100) not null,
	TypeId int not null 
	FOREIGN KEY (TypeId) REFERENCES AddressType(TypeId),
	UserId INT not null
	FOREIGN KEY (UserId) REFERENCES Users(UserId),
);

------Add Address Stored Procedure-------

create or alter proc spAddAddress
(
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
)
as
BEGIN
	If exists(select * from AddressType where TypeId=@TypeId)
		begin
			Insert into Addresses
			values(@Address, @City, @State, @TypeId, @UserId);
		end
	Else
		begin
			select 2
		end
end;

--------Get All Addresses Stored Procedure--------

create or alter Proc spGetAllAddresses
(
	@UserId int
)
as
begin
	select Address, City, State, a.UserId, b.TypeId
	from Addresses a
    Inner join AddressType b on b.TypeId = a.TypeId 
	where 
	UserId = @UserId;
end;

-------Update Address Stored Procedure------

create or alter proc spUpdateAddress
(
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
)
as
BEGIN
	If exists(select * from AddressType where TypeId = @TypeId)
		begin
			Update Addresses set
			Address = @Address, City = @City,
			State = @State, TypeId = @TypeId,
			UserId = @UserId
			where
				AddressId = @AddressId
		end
	Else
		begin
			select 2
		end
end;

-------Delete Address Stored Procedure-------

create or alter Proc spDeleteAddress
(
	@AddressId int,
	@UserId int
)
as
begin
	Delete Addresses
	where 
		AddressId=@AddressId and UserId=@UserId;
end;

select * from AddressType
select * from Addresses