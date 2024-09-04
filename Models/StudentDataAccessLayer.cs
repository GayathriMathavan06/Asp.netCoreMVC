using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace MVCCoreDemo.Models
{
    public class StudentDataAccessLayer
    {
        string connectionString = "server=127.0.0.1;uid=root;pwd=;database=school";

        // To View all Student details
        public IEnumerable<Student> GetAllStudent()
        {
            List<Student> studList = new List<Student>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("spGetAllStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Student stud = new Student
                    {
                        StudId = Convert.ToInt32(rdr["StudId"]), // Make sure column name matches exactly
                        Name = rdr["Name"].ToString(),
                        Gender = rdr["Gender"].ToString(),
                        Department = rdr["Department"].ToString(),
                        City = rdr["City"].ToString()
                    };

                    studList.Add(stud);
                }
                con.Close();
            }
            return studList;
        }

        // To Add new student record
        public void AddStudent(Student student)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("spAddStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_Name", student.Name);
                cmd.Parameters.AddWithValue("@p_City", student.City);
                cmd.Parameters.AddWithValue("@p_Department", student.Department);
                cmd.Parameters.AddWithValue("@p_Gender", student.Gender);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // To Update the records of an individual student
        public void UpdateStudent(Student student)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("spUpdateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_StudId", student.StudId);
                cmd.Parameters.AddWithValue("@p_Name", student.Name);
                cmd.Parameters.AddWithValue("@p_City", student.City);
                cmd.Parameters.AddWithValue("@p_Department", student.Department);
                cmd.Parameters.AddWithValue("@p_Gender", student.Gender);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Get the details of an individual student
        public Student GetStudentById(int id)
        {
            Student student = new Student();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblStudent WHERE StudId = @p_StudId";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@p_StudId", id);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    student.StudId = Convert.ToInt32(rdr["StudId"]);
                    student.Name = rdr["Name"].ToString();
                    student.Gender = rdr["Gender"].ToString();
                    student.Department = rdr["Department"].ToString();
                    student.City = rdr["City"].ToString();
                }
                con.Close();
            }
            return student;
        }

        // To Delete the record of a particular student
        public void DeleteStudent(int id)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("spDeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_StudId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
