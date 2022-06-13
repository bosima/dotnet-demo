// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

PersonDapperDAL dapperDAL=new PersonDapperDAL();
var personList1 = dapperDAL.GetList();

PersonAdoNetDAL adonetDAL=new PersonAdoNetDAL();
var personList2 =adonetDAL.GetList();

Console.ReadLine();