use BookStoreDB

-------Book table-------

create table Books(
BookId int identity(1,1) not null primary key,
BookName varchar(max) not null,
AuthorName varchar(250) not null,
Rating int,
RatingCount int,
DiscountPrice int not null,
OriginalPrice int not null,
Description varchar(max) not null,
BookImage varchar(250),
BookQuantity int not null
);

Alter table Books 
Add AdminId int not null Foreign key (AdminId) references Admins(AdminId) default 1;

select * from Books

---------Stored Procedures for Books-----------

------Add Book Stored Procedure------

create proc spAddBook
(
@BookName varchar(max),
@AuthorName varchar(250),
@Rating int,
@RatingCount int,
@DiscountPrice int,
@OriginalPrice int,
@Description varchar(max),
@BookImage varchar(250),
@BookQuantity int
)
as
begin
insert into Books (BookName,AuthorName,Rating,RatingCount,DiscountPrice,OriginalPrice,Description,BookImage,BookQuantity)
values(@BookName,@AuthorName,@Rating,@RatingCount,@DiscountPrice,@OriginalPrice,@Description,@BookImage,@BookQuantity);
end;

--------Update Book Stored Procedure--------

create proc spUpdateBook
(
@BookId int,
@BookName varchar(max),
@AuthorName varchar(250),
@Rating int,
@RatingCount int,
@DiscountPrice int,
@OriginalPrice int,
@Description varchar(max),
@BookImage varchar(250),
@BookQuantity int
)
as
begin
update Books set 
BookName=@BookName,
AuthorName=@AuthorName,
Rating=@Rating,
RatingCount=@RatingCount,
DiscountPrice=@DiscountPrice,
OriginalPrice=@OriginalPrice,
Description=@Description,
BookImage=@BookImage,
BookQuantity=@BookQuantity
where BookId=@BookId			
end;

--------Delete Book Stored Procedure--------

create proc spDeleteBook
(
@BookId int
)
as
begin
delete from Books Where BookId=@BookId
end;

--------Get Book by BookId stored Procedure------

create proc spGetBookById
(
@BookId int
)
as 
begin
select * from Books where BookId=@BookId
end;

-------Get All Books Stored Procedure-------

alter proc spGetAllBooks
as 
begin
select * from Books
end;

select * from Books