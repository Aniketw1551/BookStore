use BookStoreDB

--------Cart Table----------

create table Carts
(
	CartId int identity(1,1) not null primary key,
	OrderQuantity int default 1,
	UserId int foreign key references Users(UserId) on delete no action,
	BookId int foreign key references Books(BookId) on delete no action
);


select * from Carts

-------Cart Table Stored Procedures--------

-----Add In Cart Stored Procedure-----

create or alter proc spAddCart
(
@OrderQuantity int,
@UserId int,
@BookId int
)
as
begin 
		if(exists(select * from Books where BookId=@BookId))
		begin
		insert into Carts(OrderQuantity,UserId,BookId)
		values(@OrderQuantity,@UserId,@BookId)
		end
		else
		begin
		select 1
		end
end;


------GetCartByUserId Stored Procedure-----

create proc spGetCartByUser
(
	@UserId int
)
as
begin
	select CartId,OrderQuantity,UserId,c.BookId,BookName,AuthorName,
	DiscountPrice,OriginalPrice,BookImage from Carts c join Books b on c.BookId=b.BookId 
	where UserId=@UserId;
end;

-----Update Cart Stored Procedure-----

create proc spUpdateCart
(
	@OrderQuantity int,
	@BookId int,
	@UserId int,
	@CartId int
)
as
begin
update Carts set BookId=@BookId,
				UserId=@UserId,
				OrderQuantity=@OrderQuantity
				where CartId=@CartId;
end;

-----Delete Cart Stored Procedure-----

create proc spDeleteCart
(
	@CartId int,
	@BookId int
)
as
begin
delete Carts where
		CartId=@CartId and BookId=@BookId;
end;

select * from Carts