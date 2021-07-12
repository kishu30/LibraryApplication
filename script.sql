create table Member(
MemberCode int not null primary key identity(1,1),
MemberName Varchar(40) Not null,
MemberTypeId int Not null,
NumberOfBookIssued int Null default 0

);

create table Book(
BookCode int not null primary key,
BookName varchar(40) not null,
Author varchar(40),
IsIssuable bit  not null default 0,
IsIssued bit not null default 0

);


Create table MemberType(
MemberTypeId int primary key not null,
MemberType varchar(40),
NoOfBookAllowed int not null,
DaysAllowed int not null
);

Alter Table Member 
ADD FOREIGN KEY (MemberTypeId) REFERENCES MemberType(MemberTypeId);

create table  IssueBook (
IssueCode int not null primary key identity(1,1),
Membercode int not null,
BookCode int not null,
IssueDate date not null default getdate(),
DueDate date not null,
ReturnDate date not null

)

ALTER TABLE issuebook 
ALTER COLUMN returndate date null ; 

Alter Table IssueBook 
ADD FOREIGN KEY (MemberCode) REFERENCES Member(MemberCode);
Alter Table IssueBook 
ADD FOREIGN KEY (BookCode) REFERENCES  Book(BookCode);

insert into MemberType values( 1,'Student',3,10)
insert into MemberType values( 2,'Faculty',25,90)


insert into Book (bookcode,bookname,author) values (202,'Truly Devious by Maureen Johnson','Maureen Johnson');


insert into Book (bookcode,bookname,author,isissuable) values (203,' Queen’s Thief by Megan Whalen Turner','Megan Whalen',0);

insert into Member(MemberName,MemberTypeId,NumberOfBookIssued) values ('Aman',1,2)

insert into Member(MemberName,MemberTypeId,NumberOfBookIssued) values ('Priyadarshi',1,2)

insert into Member(MemberName,MemberTypeId,NumberOfBookIssued) values ('kishu',1,2)

insert into Member(MemberName,MemberTypeId,NumberOfBookIssued) values ('kumar',1,2)

insert into Member(MemberName,MemberTypeId,NumberOfBookIssued) values ('oreo',1,2)

insert into Member(MemberName,MemberTypeId,NumberOfBookIssued) values ('bklie',1,2)

insert into Member(MemberName,MemberTypeId,NumberOfBookIssued) values ('blue',1,2)



insert into Member(MemberName,MemberTypeId,NumberOfBookIssued) values ('rosy',2,20)
insert into Member(MemberName,MemberTypeId,NumberOfBookIssued) values ('dr hun',2,20)




insert into Member(MemberName,MemberTypeId,NumberOfBookIssued) values ('mr snoew',2,13)
select * from book
select * from IssueBook

insert into IssueBook (Membercode,BookCode,DueDate) values (1,108,'2021-07-17')
insert into IssueBook (Membercode,BookCode,DueDate) values (2,101,'2021-07-17')
insert into IssueBook (Membercode,BookCode,DueDate) values (3,102,'2021-07-17')
insert into IssueBook (Membercode,BookCode,DueDate) values (4,103,'2021-07-17')
insert into IssueBook (Membercode,BookCode,DueDate) values (5,104,'2021-07-17')






insert into Book (bookcode,bookname,author,isissuable) values (102,'Harry Potter and the Chamber of Secrets','J.K. Rowling',1);
insert into Book (bookcode,bookname,author,isissuable) values (103,'Harry Potter and the Prisoner of Azkaban','J.K. Rowling',1);
insert into Book (bookcode,bookname,author,isissuable) values (104,'Harry Potter and the Goblet of Fire','J.K. Rowling',1);
insert into Book (bookcode,bookname,author,isissuable) values (105,'Harry Potter and the Order of the Phoenix','J.K. Rowling',1);
insert into Book (bookcode,bookname,author,isissuable) values (106,'Harry Potter and the Half-Blood Prince','J.K. Rowling',1);
insert into Book (bookcode,bookname,author,isissuable) values (107,'Harry Potter and the Deathly Hallows','J.K. Rowling',1);
insert into Book (bookcode,bookname,author,isissuable) values (108,'Harry Potter and the Cursed Child','J.K. Rowling',1);
insert into Book (bookcode,bookname,author,isissuable) values (109,'The Tales of Beedle the Bard','J.K. Rowling',1);
select * from book
insert into Book (bookcode,isissuable) values (102,1);





select * from book
select * from Member
select * from MemberType
select * from IssueBook

update book set IsIssued = 1 where BookCode=101
update book set IsIssued = 1 where BookCode=102
update book set IsIssued = 1 where BookCode=104
update book set IsIssued = 1 where BookCode=103
update book set IsIssued= 1 where BookCode=109
update book set IsIssued= 1 where BookCode=202
update book set IsIssued = 1 where BookCode=108

sp_help'book'



