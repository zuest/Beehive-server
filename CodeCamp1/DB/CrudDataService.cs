using CodeCamp1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodeCamp1.DB
{
    public class executeReuslt
    {
        public string addedCount { get; set; }
        public Int32 result { get; set; }

    }


    public class dbConnector
    {
        private SqlConnection SqlConn = null;

        public SqlConnection GetConnection
        {
            get { return SqlConn; }
            set { SqlConn = value; }
        }

        public dbConnector()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["dbConnCRUD"].ConnectionString;
            SqlConn = new SqlConnection(ConnectionString);
        }
    }

    public class CrudDataService
    {
        //public executeReuslt InsertResponseLog(string payRes)
        //{
        //    dbConnector objConn = new dbConnector();
        //    SqlConnection Conn = objConn.GetConnection;
        //    Conn.Open();

        //    int result = 0;
        //    executeReuslt r = new executeReuslt();

        //    try
        //    {
        //        if (Conn.State != System.Data.ConnectionState.Open)
        //            Conn.Open();

        //        SqlCommand objCommand = new SqlCommand("INSERT_LOG_WEB", Conn);

        //        if (string.IsNullOrEmpty(payRes) == false)
        //        {

        //            objCommand.CommandType = CommandType.StoredProcedure;
        //            objCommand.Parameters.AddWithValue("@payRes", payRes);

        //            result = Convert.ToInt32(objCommand.ExecuteScalar());
        //        }


        //        if (result > 0)
        //        {
        //            r.memberId = "";
        //            r.refId = "";
        //            r.result = result;
        //            return r;
        //        }
        //        else
        //        {
        //            r.memberId = "Error";
        //            r.refId = "Error";
        //            r.result = 0;
        //            return r;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        r.memberId = "Error";
        //        r.refId = "Error";
        //        r.result = 0;
        //        return r;
        //    }
        //    finally
        //    {
        //        if (Conn != null)
        //        {
        //            if (Conn.State == ConnectionState.Open)
        //            {
        //                Conn.Close();
        //                Conn.Dispose();
        //            }
        //        }
        //    }
        //}

        public Trip AssignPassengerToTrip(string passengerId)
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            Trip item = new Trip();

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("[CodeCamp].[Assign_PassengerToTrip]", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@PassengerId", Convert.ToInt32(passengerId));

                objCommand.Parameters.Add("@DriverId", SqlDbType.Int);
                objCommand.Parameters["@DriverId"].Direction = ParameterDirection.Output;

                objCommand.Parameters.Add("@PickupGate", SqlDbType.Int);
                objCommand.Parameters["@PickupGate"].Direction = ParameterDirection.Output;

                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {                    

                    item.PassengerId = Convert.ToInt32(_Reader["PassengerId"]);
                    item.DriverId = Convert.ToInt32(_Reader["DriverId"]);
                    item.PickupGate = Convert.ToInt32(_Reader["PickupGate"]);
                    item.CompanyId = Convert.ToInt32(_Reader["CompanyId"]);
                    item.DriverName = _Reader["DriverName"].ToString();
                    item.DriverQID = _Reader["QID"].ToString();
                    item.PlateNum = _Reader["PlateNum"].ToString();
                }

                return item;
            }

            catch (Exception ex)
            {
                item.DriverId = -3;
                item.PickupGate = 0;
                return item;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }


        }

        public executeReuslt GenerateFlightInfoDb(string userName)
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            int result = 0;
            executeReuslt r = new executeReuslt();

            r.result = 0;
            r.addedCount = "-1";


            try
            {
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();                

                SqlCommand objCommand = new SqlCommand("[CodeCamp].[GENERATE_FLIGHTINFO]", Conn);



                objCommand.CommandType = CommandType.StoredProcedure;

                objCommand.Parameters.Add("@AddedCount", SqlDbType.Int);
                objCommand.Parameters["@AddedCount"].Direction = ParameterDirection.Output;

                result = Convert.ToInt32(objCommand.ExecuteScalar());


                if (result > 0)
                {
                    r.addedCount = objCommand.Parameters["@AddedCount"].Value.ToString();
                    r.result = result;
                    return r;
                }
                else
                {
                    r.addedCount = "-2";
                    r.result = 0;
                    return r;
                }
            }
            catch (Exception ex)
            {
                r.addedCount = "-3";
                r.result = 0;
                return r;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }


        }

        public int ReadPassengerCount()
        {
            int needRideCount = -1;

            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("[CodeCamp].Read_PassengerCount", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;

                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    needRideCount = Convert.ToInt32(_Reader["needRideCount"]);
                }

                return needRideCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }

        public List<TaxiProvider> ReadTaxiProvidersList()
        {
            List<TaxiProvider> tpList = new List<TaxiProvider>();

            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("[qpu].[Read_TaxiProviders]", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;

                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    TaxiProvider tp = new TaxiProvider();

                    tp.Id = Convert.ToInt32(_Reader["Id"]);
                    tp.Name = _Reader["Name"].ToString();
                    tp.Quote = Convert.ToInt32(_Reader["Quote"]);

                    tpList.Add(tp);
                }

                return tpList;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }

        //public executeReuslt InsertStudentMark(StudentMark objStuMark, string userName)
        //{
        //    dbConnector objConn = new dbConnector();
        //    SqlConnection Conn = objConn.GetConnection;
        //    Conn.Open();

        //    int result = 0;
        //    executeReuslt r = new executeReuslt();

        //    r.result = 0;
        //    r.markId = "-5";


        //    try
        //    {
        //        if (Conn.State != System.Data.ConnectionState.Open)
        //            Conn.Open();

        //        //if (objStuMark.CustNotes == null)
        //        //{
        //        //    objCust.CustNotes = "";
        //        //}

        //        //if (objCust.CustBookDate == null)
        //        //{
        //        //    objCust.CustBookDate = "";
        //        //}

        //        //objCust.CustRequiredAmount = -1;

        //        float totalMark = (objStuMark.ActivityMark + objStuMark.MiddleExamMark + objStuMark.FinalExamMark);

        //        if (totalMark != objStuMark.TotalMark)
        //            return r;

        //        string es = "error";

        //        if ((98 <= totalMark) && (totalMark <= 100))
        //            es = "A+";

        //        else if ((95 <= totalMark) && (totalMark < 98))
        //            es = "A";

        //        else if ((90 <= totalMark) && (totalMark < 95))
        //            es = "A-";

        //        else if ((85 <= totalMark) && (totalMark < 90))
        //            es = "B+";

        //        else if ((80 <= totalMark) && (totalMark < 85))
        //            es = "B";

        //        else if ((75 <= totalMark) && (totalMark < 80))
        //            es = "B-";

        //        else if ((70 <= totalMark) && (totalMark < 75))
        //            es = "C+";

        //        else if ((65 <= totalMark) && (totalMark < 70))
        //            es = "C";

        //        else if ((60 <= totalMark) && (totalMark < 65))
        //            es = "C-";

        //        else if ((55 <= totalMark) && (totalMark < 60))
        //            es = "D+";

        //        else if ((50 <= totalMark) && (totalMark < 55))
        //            es = "D";

        //        else if ((0 < totalMark) && (totalMark < 50))
        //            es = "F";

        //        else if (totalMark == -3)
        //            es = "Z";

        //        else if (totalMark == -6)
        //            es = "W";

        //        else if (totalMark == -9)
        //            es = "l";



        //        if (es != objStuMark.FinalEstimation)
        //            return r;

        //        SqlCommand objCommand = new SqlCommand("[qpu].CREATE_STUDENTMARK_WEB", Conn);

        //        if (objStuMark.TotalMark > -10)
        //        {

        //            objCommand.CommandType = CommandType.StoredProcedure;
        //            objCommand.Parameters.AddWithValue("@CourseId", objStuMark.CourseId);
        //            objCommand.Parameters.AddWithValue("@SemesterId", objStuMark.SemesterId);
        //            objCommand.Parameters.AddWithValue("@StudentNum", objStuMark.StudentNum);
        //            objCommand.Parameters.AddWithValue("@StudentName", objStuMark.StudentName);
        //            objCommand.Parameters.AddWithValue("@MiddleExamMark", objStuMark.MiddleExamMark);
        //            objCommand.Parameters.AddWithValue("@ActivityMark", objStuMark.ActivityMark);
        //            objCommand.Parameters.AddWithValue("@FinalExamMark", objStuMark.FinalExamMark);
        //            objCommand.Parameters.AddWithValue("@TotalMark", totalMark);
        //            objCommand.Parameters.AddWithValue("@FinalEstimation", es);
        //            objCommand.Parameters.AddWithValue("@UserId", userName);

        //            objCommand.Parameters.Add("@MarkId", SqlDbType.Int);
        //            objCommand.Parameters["@MarkId"].Direction = ParameterDirection.Output;

        //            result = Convert.ToInt32(objCommand.ExecuteScalar());
        //        }


        //        if (result > 0)
        //        {
        //            r.markId = objCommand.Parameters["@MarkId"].Value.ToString();
        //            r.result = result;
        //            return r;
        //        }
        //        else
        //        {
        //            r.markId = "-2";
        //            r.result = 0;
        //            return r;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        r.markId = "-3";
        //        r.result = 0;
        //        return r;
        //    }
        //    finally
        //    {
        //        if (Conn != null)
        //        {
        //            if (Conn.State == ConnectionState.Open)
        //            {
        //                Conn.Close();
        //                Conn.Dispose();
        //            }
        //        }
        //    }
        //}



        //public List<Course> ReadCourseList(int facultyId)
        //{
        //    List<Course> cList = new List<Course>();

        //    dbConnector objConn = new dbConnector();
        //    SqlConnection Conn = objConn.GetConnection;
        //    Conn.Open();

        //    try
        //    {
        //        if (Conn.State != System.Data.ConnectionState.Open)
        //            Conn.Open();

        //        SqlCommand objCommand = new SqlCommand("[qpu].VIEW_COURSE_WEB", Conn);
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.Parameters.AddWithValue("@FacultyId", facultyId);

        //        SqlDataReader _Reader = objCommand.ExecuteReader();

        //        while (_Reader.Read())
        //        {
        //            Course item = new Course();

        //            item.Id = Convert.ToInt32(_Reader["Id"]);
        //            item.Code = _Reader["Code"].ToString();
        //            item.Name = _Reader["Name"].ToString();
        //            item.FacultyId = Convert.ToInt32(_Reader["FacultyId"]);

        //            cList.Add(item);

        //        }

        //        return cList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (Conn != null)
        //        {
        //            if (Conn.State == ConnectionState.Open)
        //            {
        //                Conn.Close();
        //                Conn.Dispose();
        //            }
        //        }
        //    }
        //}

        //public List<Faculty> ReadFacultyList(int userId)
        //{
        //    List<Faculty> fList = new List<Faculty>();

        //    dbConnector objConn = new dbConnector();
        //    SqlConnection Conn = objConn.GetConnection;
        //    Conn.Open();

        //    try
        //    {
        //        if (Conn.State != System.Data.ConnectionState.Open)
        //            Conn.Open();

        //        SqlCommand objCommand = new SqlCommand("[qpu].VIEW_FACULTY_WEB", Conn);
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.Parameters.AddWithValue("@UserId", userId);

        //        SqlDataReader _Reader = objCommand.ExecuteReader();

        //        while (_Reader.Read())
        //        {
        //            Faculty item = new Faculty();

        //            item.Id = Convert.ToInt32(_Reader["Id"]);
        //            item.Code = _Reader["Code"].ToString();
        //            item.Name = _Reader["Name"].ToString();

        //            fList.Add(item);

        //        }

        //        return fList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (Conn != null)
        //        {
        //            if (Conn.State == ConnectionState.Open)
        //            {
        //                Conn.Close();
        //                Conn.Dispose();
        //            }
        //        }
        //    }
        //}

        //public int ReadUserLogin(string name, string pass)
        //{
        //    int isFound = 0;

        //    dbConnector objConn = new dbConnector();
        //    SqlConnection Conn = objConn.GetConnection;
        //    Conn.Open();

        //    try
        //    {
        //        if (Conn.State != System.Data.ConnectionState.Open)
        //            Conn.Open();

        //        SqlCommand objCommand = new SqlCommand("[qpu].[LoginUser]", Conn);
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.Parameters.AddWithValue("@UserName", name);
        //        objCommand.Parameters.AddWithValue("@Password", pass);

        //        isFound = Convert.ToInt32(objCommand.ExecuteScalar());

        //        if (isFound > 0)
        //            return 1;


        //        return isFound;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (Conn != null)
        //        {
        //            if (Conn.State == ConnectionState.Open)
        //            {
        //                Conn.Close();
        //                Conn.Dispose();
        //            }
        //        }
        //    }
        //}


        //public List<StudentMark> ReadStudentMarksByCourseList(int courseId, int semesterId)
        //{
        //    List<StudentMark> smList = new List<StudentMark>();

        //    dbConnector objConn = new dbConnector();
        //    SqlConnection Conn = objConn.GetConnection;
        //    Conn.Open();

        //    try
        //    {
        //        if (Conn.State != System.Data.ConnectionState.Open)
        //            Conn.Open();

        //        SqlCommand objCommand = new SqlCommand("[qpu].VIEW_STUDENTMARKS_WEB", Conn);
        //        objCommand.CommandType = CommandType.StoredProcedure;

        //        objCommand.Parameters.AddWithValue("@CourseId", courseId);
        //        objCommand.Parameters.AddWithValue("@SemesterId", semesterId);

        //        SqlDataReader _Reader = objCommand.ExecuteReader();

        //        while (_Reader.Read())
        //        {
        //            StudentMark item = new StudentMark();

        //            string s = " الفصل الثاني 2018-2019";
        //            item.CourseId = Convert.ToInt32(_Reader["CourseId"]);
        //            item.CourseName = (_Reader["CourseName"]).ToString().Trim();
        //            item.SemesterId = Convert.ToInt32(_Reader["SemesterId"]);
        //            item.SemesterName = s;
        //            item.StudentNum = Convert.ToInt64(_Reader["StudentNum"]);
        //            item.StudentName = _Reader["StudentName"].ToString();
        //            item.MiddleExamMark = float.Parse(_Reader["MiddleExamMark"].ToString());
        //            item.ActivityMark = float.Parse(_Reader["ActivityMark"].ToString());
        //            item.FinalExamMark = float.Parse(_Reader["FinalExamMark"].ToString());
        //            item.TotalMark = float.Parse(_Reader["TotalMark"].ToString());
        //            item.FinalEstimation = _Reader["FinalEstimation"].ToString();

        //            smList.Add(item);

        //        }

        //        return smList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (Conn != null)
        //        {
        //            if (Conn.State == ConnectionState.Open)
        //            {
        //                Conn.Close();
        //                Conn.Dispose();
        //            }
        //        }
        //    }
        //}

        //public string GetRefByMemberId(string memberId)
        //{

        //    dbConnector objConn = new dbConnector();
        //    SqlConnection Conn = objConn.GetConnection;
        //    Conn.Open();

        //    try
        //    {
        //        string refId = "";

        //        if (Conn.State != System.Data.ConnectionState.Open)
        //            Conn.Open();

        //        SqlCommand objCommand = new SqlCommand("GET_REFBYMEMBER_WEB", Conn);
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.Parameters.AddWithValue("@MemberID", memberId);
        //        SqlDataReader _Reader = objCommand.ExecuteReader();

        //        while (_Reader.Read())
        //        {
        //            refId = _Reader["CustReferenceID"].ToString();
        //        }

        //        return refId;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (Conn != null)
        //        {
        //            if (Conn.State == ConnectionState.Open)
        //            {
        //                Conn.Close();
        //                Conn.Dispose();
        //            }
        //        }
        //    }
        //}

    }
}