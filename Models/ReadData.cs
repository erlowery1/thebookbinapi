using System.Data.Common;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel.Design;
using System.Transactions;
using System;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SQLite;
using api.Models.Interfaces;

namespace api.Models
{
    public class ReadData : IGetAll, IGet
    {
        //returns a list of all books in the database
        public List<Book> GetAllBooks()
        {
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if(isOpen){
                MySqlConnection conn = db.GetConn();
                string stm = "SELECT * from books";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                List<Book> allBooks = new List<Book>();
                using(var rdr = cmd.ExecuteReader()){
                    while(rdr.Read()){
                        allBooks.Add(new Book{Id = rdr.GetInt32(0), Isbn = rdr.GetString(1), Title = rdr.GetString(2), Author = rdr.GetString(3), Genre = rdr.GetString(4), Price = rdr.GetDouble(5)});
                    }
                }
                db.CloseConnection(); //close connection
                return allBooks;
            }else{
                return new List<Book>();
            }
            // string directory = Directory.GetCurrentDirectory();
            // string cs = @"URI = file:"+ directory+ @"/bookbin.db";
            // using var con = new SQLiteConnection(cs);
            // con.Open();

            // string stm = "SELECT * from books";
            // using var cmd = new SQLiteCommand(stm, con);

            // using SQLiteDataReader rdr = cmd.ExecuteReader();

            // List<Book> allBooks = new List<Book>();
            // while(rdr.Read()){
            //     allBooks.Add(new Book{Id = rdr.GetInt32(0), Isbn = rdr.GetInt32(1), Title = rdr.GetString(2), Author = rdr.GetString(3), Genre = rdr.GetString(4), Price = rdr.GetDouble(5)});
            // }

            // return allBooks;
        }

        //finds a book with the matching id and returns the data
        public Book GetBook(int Id)
        {
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if(isOpen){
                MySqlConnection conn = db.GetConn();
                string stm = "SELECT * from books WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                cmd.Parameters.AddWithValue("id", Id);
                cmd.Prepare();
                using(var rdr = cmd.ExecuteReader()){
                    rdr.Read();
                    //db.CloseConnection(); //close connection
                    return new Book(){Id = rdr.GetInt32(0), Isbn = rdr.GetString(1), Title = rdr.GetString(2), Author = rdr.GetString(3), Genre = rdr.GetString(4), Price = rdr.GetDouble(5)};
                }
                
            }else{
                return new Book();
            }
            // string directory = Directory.GetCurrentDirectory();
            // //Console.WriteLine(directory);
            // string cs = @"URI = file:"+ directory+ @"/bookbin.db";
            // using var con = new SQLiteConnection(cs);
            // con.Open();

            // string stm = "SELECT * from books WHERE id = @id";
            // using var cmd = new SQLiteCommand(stm, con);
            // cmd.Parameters.AddWithValue("id", Id);
            // cmd.Prepare();
            // using SQLiteDataReader rdr = cmd.ExecuteReader();

            // rdr.Read();
            // return new Book(){Id = rdr.GetInt32(0), Isbn = rdr.GetString(1), Title = rdr.GetString(2), Author = rdr.GetString(3), Genre = rdr.GetString(4), Price = rdr.GetDouble(5)};

        }


        public List<Transaction> GetAllTransactions()
        {
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if(isOpen){
                MySqlConnection conn = db.GetConn();
                string stm = "SELECT * from transactions";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                List<Transaction> alltransactions = new List<Transaction>();
                using(var rdr = cmd.ExecuteReader()){
                    while(rdr.Read()){
                        alltransactions.Add(new Transaction{Id = rdr.GetInt32(0), Isbn = rdr.GetString(1), Title = rdr.GetString(2), Author = rdr.GetString(3), Genre = rdr.GetString(4), Price = rdr.GetDouble(5), Name = rdr.GetString(6), Date = rdr.GetDateTime(7)});
                    }
                }
                db.CloseConnection(); //close connection
                return alltransactions;
            }else{
                return new List<Transaction>();
            }
            // string directory = Directory.GetCurrentDirectory();
            // string cs = @"URI = file:"+ directory+ @"/bookbin.db";
            // using var con = new SQLiteConnection(cs);
            // con.Open();

            // string stm = "SELECT * from transactions";
            // using var cmd = new SQLiteCommand(stm, con);

            // using SQLiteDataReader rdr = cmd.ExecuteReader();

            // List<Transaction> allTransactions = new List<Transaction>();
            // while(rdr.Read()){
            //     allTransactions.Add(new Transaction{Id = rdr.GetInt32(0), Isbn = rdr.GetString(1), Title = rdr.GetString(2), Author = rdr.GetString(3), Genre = rdr.GetString(4), Price = rdr.GetDouble(5), Name = rdr.GetString(6), Date = rdr.GetDateTime(7)});
            // }

            // return allTransactions;
        }
    }
}