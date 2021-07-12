using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApplication.Commands;
using LibraryApplication.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace LibraryApplication.ViewModels
{
    public class LibraryViewModel : INotifyPropertyChanged
    {
        LibraryDataEntities db;
        ObservableCollection<Book> bookslist = null;


        #region inotifyproperty
        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


        #region ctor

        public LibraryViewModel()
        {
            db = new LibraryDataEntities();

            
            bookslist = new ObservableCollection<Book>();

            issueCommand = new RelayCommands(IssueMethod, CanIssue);

            clearCommand = new RelayCommands(ClearMethod, canclear);

        }

        #endregion


       #region properties

        public ObservableCollection<Book> BookList
        {
            get { return bookslist; }

            set
            {
                bookslist = value;
                OnPropertyChanged("BookList");
            }
        }







        private Member memberobject = new Member();

        public string UI_MemberCode
        {
            get { return memberobject.MemberCode.ToString(); }
            set
            {
                if (value != string.Empty)
                {
                    memberobject.MemberCode = Convert.ToInt32(value);
                    get_member_details(Convert.ToInt32(value));
                    OnPropertyChanged("UI_MemberCode");
                }
            }
        }


        public string UI_MemmberName
        {
            get { return memberobject.MemberName; }
            set
            {
                memberobject.MemberName = value;
                OnPropertyChanged("UI_MemmberName");
            }
        }

        private MemberType memberTypeobject = new MemberType();
        public string UI_MemberType
        {
            get { return memberTypeobject.MemberType1; }
            set
            {
                memberTypeobject.MemberType1 = value;
                OnPropertyChanged("UI_MemberType");
            }
        }

        public string UI_NoOfBookAllowed
        {
            get { return memberTypeobject.NoOfBookAllowed.ToString(); }
            set
            {
                memberTypeobject.NoOfBookAllowed = Convert.ToInt32(value);
                OnPropertyChanged("UI_NoOfBookAllowed");
            }
        }

        public string UI_NoOfBookIssued
        {
            get { return memberobject.NumberOfBookIssued.ToString(); }


            set
            {
                memberobject.NumberOfBookIssued = Convert.ToInt32(value);
                OnPropertyChanged("UI_NoOfBookIssued");
            }
        }


        private Book bookobject = new Book();

        public string UI_BookCode
        {
            get { return bookobject.BookCode.ToString(); }

            set
            {
                if (value != string.Empty)
                {
                    bookobject.BookCode = Convert.ToInt32(value);
                    get_book_details(Convert.ToInt32(value));
                    OnPropertyChanged("UI_BookCode");
                }
            }
        }

        public string UI_BookName
        {
            get { return bookobject.BookName; }

            set
            {
                bookobject.BookName = value;
                OnPropertyChanged("UI_BookName");
            }
        }
        public string UI_BookAuthor
        {
            get { return bookobject.Author; }

            set
            {
                bookobject.Author = value;
                OnPropertyChanged("UI_BookAuthor");
            }
        }





        private IssueBook IssueBookobject = new IssueBook();

        public string UI_Issuedate
        {
            get { return IssueBookobject.IssueDate.ToShortDateString(); }

            set
            {
                IssueBookobject.IssueDate = Convert.ToDateTime(value);
                OnPropertyChanged("UI_Issuedate");
            }
        }

        public string UI_DueDateOfreturn
        {
            get { return IssueBookobject.DueDate.ToString() ; }

            set
            {
                IssueBookobject.DueDate = Convert.ToDateTime(value);
                OnPropertyChanged("UI_DueDateOfreturn");
            }
        }


        public string UI_Actualdateofreturn
        {
            get { return IssueBookobject.ReturnDate.ToString(); }

            set
            {
                IssueBookobject.ReturnDate = Convert.ToDateTime(value);
                OnPropertyChanged("UI_Actualdateofreturn");
            }
        }


        #endregion


        #region Command 

        private ICommand issueCommand;

        public ICommand IssueCommand
        {
            get { return issueCommand; }
            set { issueCommand = value; }
        }

        private ICommand clearCommand;

        public ICommand ClearCommand
        {
            get { return clearCommand; }
            set { clearCommand = value; }
        }



        #endregion

        #region methods

        public void IssueMethod(object obj)
        {
            


            // issued in book
            // no of book issued in member
            // insert new row in issuetable

            IssueBook newissuebook = new IssueBook();

            newissuebook.Membercode = Convert.ToInt32(UI_MemberCode);
            newissuebook.BookCode = Convert.ToInt32(UI_BookCode);
            newissuebook.IssueDate = Convert.ToDateTime(UI_Issuedate);
            newissuebook.DueDate = Convert.ToDateTime(UI_DueDateOfreturn);
            db.IssueBooks.Add(newissuebook);
            db.SaveChanges();

            Member updatememberbookcount = new Member();

            updatememberbookcount = db.Members.Where(m => m.MemberCode.ToString() == UI_MemberCode).SingleOrDefault();

            if (updatememberbookcount != null)
            {
                updatememberbookcount.NumberOfBookIssued += 1;
                db.SaveChanges(); ;
            }

            Book updatebookissued = new Book();


            if (updatebookissued != null)
            {
                updatebookissued.IsIssued = true;
                db.SaveChanges(); ;
            }


        }

        public bool CanIssue(object obj)
        {
            // membercode valid, else display error  
            // student book issue should bhe less than three
            // fac hav 25 book limit
            // is book issuable





           



            return true;
        }




        public void ClearMethod(object obj)
        {

        }




        public bool canclear(object obj)
        {
            return true;
        }

        


        public void get_member_details(int membercode)
        {
            var memberdetails = (from m in db.Members join mt in db.MemberTypes on m.MemberTypeId equals mt.MemberTypeId select new { m.MemberCode, m.MemberName, m.MemberType, mt.NoOfBookAllowed, m.NumberOfBookIssued }).Where(x => x.MemberCode == membercode).SingleOrDefault();

            UI_MemmberName = memberdetails.MemberName;

            UI_NoOfBookIssued = memberdetails.NumberOfBookIssued.ToString();
            UI_NoOfBookAllowed = memberdetails.NoOfBookAllowed.ToString();






        }

        public void get_book_details(int bookcode)
        {
            var query = (from b in db.Books
                         join i in db.IssueBooks on b.BookCode equals i.BookCode
                         select new { b.BookCode, b.BookName, b.Author, i.IssueDate, i.ReturnDate }).Where(book => book.BookCode == bookcode).ToList().SingleOrDefault();

            UI_BookName = query.BookName;
            UI_BookAuthor = query.Author;
            //UI_Issuedate = query.IssueDate.ToString();
            //UI_Actualdateofreturn = query.ReturnDate.ToString();

        }

        #endregion

    }
}
