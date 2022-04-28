use BookStoreDB;

------Admin Table------

create Table Admins
(
	AdminId int identity(1,1) primary key,
	FullName varchar(max) not null,
	Email varchar(max) not null,
	Password varchar(max) not null,
	PhoneNumber varchar(20) not null
);

-----Adding Admin------
Insert into Admins values('ADMIN','admin123@gmail.com', 'Admin123', '8401290217');

select * from Admins


--------Admin Login Stored Procedure------

create or alter proc spAdminLogin
(
	@Email varchar(max),
	@Password varchar(max)
)
as
begin
     if(exists(select * from Admins where Email = @Email and Password = @Password))
          begin 
	          select AdminId, FullName, Email, PhoneNumber from Admins;
          end
 else 
        begin
	        select 2;
	   end
end;

exec spAdminLogin 
@Email = 'admin123@gmail.com' ,
@Password = 'Admin123';