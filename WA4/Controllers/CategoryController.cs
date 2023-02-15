using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WA4.Data;
using WA4.Models;
using System;
using System.Data;
using System.Data.OracleClient;
using System.IO.Ports;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace WA4.Controllers
{
    
    public class CategoryController : Controller
    {
        
        static SerialPort _serialPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
        static string[] barcode;
        public String check;
        public string changeState;
        public static string codeValue = "";
        public int changeValue;
        public string state = "checked";
        public List<double> testlist = new List<double>();
        private readonly ApplicationDbContext _db;
        public static string pn_No;
        //public IEnumerable<Category> categoryFromDb;
        IEnumerable<Category> newpageList;
        IEnumerable<Category> pageList;



        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
            
        }
        //public IEnumerable<Category> categoryFromDb = _db.Categories1;
        public IActionResult Index()
        {
            getPorts();
            IEnumerable<Category> objCategoryList = _db.Categories1;
            return View(objCategoryList);
        }

       
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectedPN(String nameobj)
        {

            
            pn_No= nameobj;
            //Taskdatabas();
            //_serialPort.PortName = "COM3";
            //_serialPort.BaudRate = 115200;
            //_serialPort.Parity = Parity.None;
            //_serialPort.DataBits = 8;
            //_serialPort.StopBits = StopBits.One;
            //_serialPort.Encoding = "UTF-8";


            // Set the read/write timeouts
            //_serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            _serialPort.DataReceived += new SerialDataReceivedEventHandler(mySerialPort_Data);

            if (!_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Open();

                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex);
                }
            }

            ViewBag.check9 = codeValue;
            List<double> newList = new List<double>();
            var num = 0;
            var bill = 4;
            List<double> duplicates = new List<double>();

            if (pageList is not null)
            {

                var myDb = pageList.Where(j => j.MaterialNumber == nameobj).ToList();


                foreach (var obj in myDb)
                {
                    newList.Add(obj.ComponentNumber);
                }
                num = newList.Count;
                duplicates = newList.GroupBy(x => x)
                                .SelectMany(g => g.Skip(1))
                                .Distinct()
                                .ToList();
                if (changeState == "false")
                {
                    num -= 1;
                }

                if (bill == 1)
                {
                    check = "background-color: #00000;";

                }
                else
                {
                    check = "background-color: #ff0000;";
                }

                ViewBag.check1 = check;
                //testlist.Add(Convert.ToDouble(data1.X2));
                ViewBag.check1 = check;
                ViewBag.check2 = "background-color: #00FF00;";
                ViewBag.check3 = 3;
                ViewBag.check4 = num;
                //ViewBag.check5 = '';
                ViewBag.update = num;
                changeValue = num;
                ViewBag.check6 = testlist;
                ViewBag.check7 = duplicates;

                return View("SelectedPNUpdate.html", pageList);
            }

            else
            {
                

                pageList = _db.Categories1.Where(j => j.MaterialNumber == nameobj).ToList();


                foreach (var obj in pageList)
                {
                    newList.Add(obj.ComponentNumber);
                }
                num = newList.Count;
                duplicates = newList.GroupBy(x => x)
                                .SelectMany(g => g.Skip(1))
                                .Distinct()
                                .ToList();
                if (changeState == "false")
                {
                    num -= 1;
                }

                if (bill == 1)
                {
                    check = "background-color: #00000;";

                }
                else
                {
                    check = "background-color: #ff0000;";
                }

                ViewBag.check1 = check;
                //testlist.Add(Convert.ToDouble(data1.X2));
                ViewBag.check1 = check;
                ViewBag.check2 = "background-color: #00FF00;";
                ViewBag.check3 = 3;
                ViewBag.check4 = num;
                //ViewBag.check5 = '';
                ViewBag.update = num;
                changeValue = num;
                ViewBag.check6 = testlist;
                ViewBag.check7 = duplicates;



                //return View(pageList);

            }
            return View("SelectedPN", pageList);
        }
        
        //GET
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {

            if (obj.MaterialNumber == obj.ComponentNumber.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot be the same as the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories1.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "PN added Successfully";
                return RedirectToAction("Index");

            }
            return View();

        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories1.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories1.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "PN updated Successfully";
                return RedirectToAction("Index");

            }
            return View();

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories1.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories1.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories1.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "PN deleted Successfully";
            return RedirectToAction("Index");


            //return View();

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchResults(String SearchPhrase)
        {
            var obj = _db.Categories1.Find(SearchPhrase);
            if (obj == null)
            {
                return NotFound();
            }
            return View("Index", _db.Categories1.Where(j => j.MaterialNumber.Contains(SearchPhrase)));


            //return View();

        }

        //POST from javascript function
       
        public ActionResult checker(SetVal data1)
        {
            changeState = data1.X1;
            state = data1.X2;
            //ViewBag.update = changeValue;
            
            
            return RedirectToAction("SelectedPN"); 

        }


        public static void getPorts()
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            Console.WriteLine("The following serial ports were found:");

            // Display each port name to the console.
            foreach (string port in ports)
            {
                Console.WriteLine(port);
            }

            Console.ReadLine();

            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            //Thread readThread = new Thread();

            // Create a new SerialPort object with default settings.
            

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = "COM3";
            _serialPort.BaudRate = 9600;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            
            // Set the read/write timeouts
            //_serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            //_serialPort.DataReceived += new SerialDataReceivedEventHandler(mySerialPort_Data);
            try
            {
                _serialPort.Open();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }


           

        }
        public ActionResult GetCurrent()
        {
            var compNum = codeValue;
            return Json(compNum, new System.Text.Json.JsonSerializerOptions());
        }

        public void mySerialPort_Data(object sender, SerialDataReceivedEventArgs e)
        {
            if (_serialPort.ReadExisting() != null)
            {
                newpageList = _db.Categories1;
                string data = _serialPort.ReadExisting();
                barcode = data.Split(";");
                codeValue = barcode[0].Substring(barcode[0].IndexOf(":") + 1);
                GetCurrent();
                

                //SelectedPN(pn_No);
            }
            
            //_serialPort.Close();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Action Update(string AddManual)
        {
            codeValue = AddManual;
            //GetCurrent();
            //var obj = _db.Categories1.Find(SearchPhrase);
            SelectedPN(pn_No);

            return null;

        }


































        /* string oradb = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-4K41ID9)(PORT = 1521))" +
             "(CONNECT_DATA =" + "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = books)));" +
             "User Id=SYSTEM;Password=YourPassword;";

         protected void Button1_Click(object sender, EventArgs e)
         {
             OracleConnection Con = new OracleConnection(oradb);
             Con.Open();
             DataTable tab = new DataTable();
             OracleDataAdapter da = new OracleDataAdapter("select * from BOOKSTABLE", Con);
             da.Fill(tab);
             GridView1.DataSource = tab;
             GridView1.DataBind();
             Con.Close();
             Con.Dispose();
         } */
    }
}