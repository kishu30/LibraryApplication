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
    public class LibraryViewModelForReturnWindow
    {
        LibraryDataEntities db;

        #region ctor
        public LibraryViewModelForReturnWindow()
        {
            db = new LibraryDataEntities();

            submitCommand = new RelayCommands(SubmitMethod, cando);

        }
        #endregion
        



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


        #region properties

        private IssueBook IssueBookobject = new IssueBook();

        public string UI_IssueCode
        {
            get { return IssueBookobject.IssueCode.ToString(); }

            set
            {
                if (value!=null)
                {
                    IssueBookobject.IssueCode = Convert.ToInt32(value);
                }

                OnPropertyChanged("UI_IssueCode");
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
            get { return IssueBookobject.DueDate.ToShortDateString(); }

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


        #region commands
        private ICommand submitCommand;

        public ICommand SubmitCommand
        {
            get { return submitCommand; }
            set { submitCommand = value; }
        }
        #endregion



        #region methods

        public bool cando(object obj)
        {
            return true;
        }


        public void SubmitMethod(object obj)
        {
            //update book table
            //member mai count kam hoga
            // issuedate mai returndate update hoga

            Member updatememberbookcount = new Member();

            updatememberbookcount = db.Members.Where(m => m.MemberCode.ToString() == UI_MemberCode).SingleOrDefault();

            if (updatememberbookcount != null)
            {
                updatememberbookcount.NumberOfBookIssued -= 1;
                db.SaveChanges(); ;
            }
            Book updatebookissued = new Book();

            updatebookissued = db.Books.Where(b => b.BookCode.ToString() == UI_BookCode).SingleOrDefault();


            if (updatebookissued != null)
            {
                updatebookissued.IsIssued = false;
                db.SaveChanges(); ;
            }

            IssueBook updatereurndateofissuebook = new IssueBook();

            updatereurndateofissuebook = db.IssueBooks.Where(i => i.IssueCode.ToString() == UI_IssueCode).SingleOrDefault();

            if (updatereurndateofissuebook != null)
            {
                updatereurndateofissuebook.ReturnDate = Convert.ToDateTime(UI_Actualdateofreturn);

            }



        }



        public void get_member_details(int membercode)
        {
            var memberdetails = (from m in db.Members join mt in db.MemberTypes on m.MemberTypeId equals mt.MemberTypeId select new { m.MemberCode, m.MemberName, m.MemberType, mt.NoOfBookAllowed, m.NumberOfBookIssued }).Where(x => x.MemberCode == membercode).SingleOrDefault();

            UI_MemmberName = memberdetails.MemberName;

            //UI_NoOfBookIssued = memberdetails.NumberOfBookIssued.ToString();
           // UI_NoOfBookAllowed = memberdetails.NoOfBookAllowed.ToString();






        }

        public void get_book_details(int bookcode)
        {
            var query = (from b in db.Books
                         join i in db.IssueBooks on b.BookCode equals i.BookCode
                         select new { b.BookCode, b.BookName, b.Author, i.IssueDate, i.ReturnDate }).Where(book => book.BookCode == bookcode).ToList().SingleOrDefault();

            UI_BookName = query.BookName;
            //UI_BookAuthor = query.Author;
            UI_Issuedate = query.IssueDate.ToString();
            UI_Actualdateofreturn = query.ReturnDate.ToString();

        }



        #endregion


    }
}
