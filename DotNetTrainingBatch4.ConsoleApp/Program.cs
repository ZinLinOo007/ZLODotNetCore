// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch4.ConsoleApp;
using System.Data;
using System.Data.SqlClient;
using ZLODotNetCore.ConsoleApp.EFCoreExamples;


//Ctr + . => suggestion
//F10
//F11 detail
//F9 => BreakPoint

//SqlConnectionStringBuilder stringBuilder =  new SqlConnectionStringBuilder();
//stringBuilder.DataSource = "ZLO\\ZLO";//servername
//stringBuilder.InitialCatalog = "DotNetTrainingBatch4" ;//database name
//stringBuilder.UserID = "sa";
//stringBuilder.Password = "015427";
//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
//connection.Open();
//Console.WriteLine("Connection  open.");
//String query = "select * from Tbl_Blog";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//adapter.Fill(dt);
//connection.Close();

////dataset => datatable
////datatabel => datarow
////datarow =>  datacolumn

//foreach(DataRow dr in dt.Rows)
//{
//    Console.WriteLine("Blog ID => " + dr["BlogId"]);
//    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
//    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
//    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
//    Console.WriteLine("-------------------------------------------");

//}
//Console.WriteLine("Connection close.");
//Console.ReadKey();


//AdoDoetNet Read
//CRUD

//AdoDotNetExample example = new AdoDotNetExample();
//example.Read();
//example.Creat("Test Title", "Test Author", "Test Content");
//example.Update(12, "Title", "Author", "Content");
//example.Delete(13);
//example.Edit(13);
//example.Edit(1);

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Run();
