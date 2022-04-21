use BookStoreDB

-----Wishlist Table-----

create table Wishlist
(
	WishlistId int identity(1,1)not null primary key,
	UserId int not null foreign key references Users(UserId) on delete no action,
	bookId int not null foreign key references Books(bookId) on delete no action
);

------Wishlist Table Stored Procedure------

-----Add In Wishlist Stored Procedure-----

create or alter proc spAddWishlist
(
	@UserId int,
	@BookId int
)
as
begin
	if exists(Select * from Wishlist where UserId = @UserId and BookId = @BookId)
		begin 
			select 2;
		end
	else
		begin 
			if exists(select * from Books where BookId = @BookId)
				begin
					Insert into Wishlist(UserId, BookId)
					values (@UserId, @BookId);
				end
			else
				begin
					select 1;
				end
			end
end;

------Get All Entries Wishlist Stored Procedure------

create or alter proc spGetAllEntriesFromWishlist
(
	@UserId int
)
as
begin
	select w.WishlistId,w.UserId,w.BookId,
	b.BookName,b.AuthorName,b.DiscountPrice,b.OriginalPrice,
	b.BookImage
	from Wishlist w
	Inner join Books b
	on w.BookId = b.BookId
	where 
		UserId = @UserId;
end;

-----Delete Wishlist Stored Procedure-----

create or alter proc spDeleteWishlist
(
@WishlistId int,
@UserId int
)
as
begin
delete Wishlist where WishlistId=@WishlistId and UserId=@UserId;
end;

select * from Wishlist