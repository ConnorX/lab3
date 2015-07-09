using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using comp2007_lesson9_lab3.Models;
using System.Web.ModelBinding;

namespace comp2007_lesson9_lab3
{
    public partial class course : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDepartments();

                //get the course if editing
                if (!String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    GetCourse();
                }
            }
        }

        protected void GetCourse()
        {
            //populate the existing course for editing
            using (DefaultConnection db = new DefaultConnection())
            {
                Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

                Course objC = (from c in db.Courses
                               where c.CourseID == CourseID
                               select c).FirstOrDefault();

                //populate the form
                txtTitle.Text = objC.Title;
                txtCredits.Text = objC.Credits.ToString();
                ddlDepartment.SelectedValue = objC.DepartmentID.ToString();

                var objS = (from s in db.Students
                            join en in db.Enrollments on s.StudentID equals en.StudentID
                            join c in db.Courses on en.CourseID equals c.CourseID
                            where en.CourseID == CourseID
                            select new { s.StudentID, s.LastName, s.FirstMidName, s.EnrollmentDate });

                grdStudents.DataSource = objS.ToList();
                grdStudents.DataBind();

                //clear dropdowns
                ddlStudent.ClearSelection();

                var stud = from s in db.Students
                           join en in db.Enrollments on s.StudentID equals en.StudentID
                           join c in db.Courses on en.CourseID equals c.CourseID
                           where c.CourseID != CourseID && en.StudentID == s.StudentID
                           orderby s.LastName
                           select s;

                ddlStudent.DataSource = stud.ToList();
                ddlStudent.DataBind();

                ListItem newItem = new ListItem("-Select-", "0");
                ddlStudent.Items.Insert(0, newItem);
                //show the course panel 
                pnlCourses.Visible = true;
            }
        }

        protected void GetDepartments()
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                var deps = (from d in db.Departments
                            orderby d.Name
                            select d);

                ddlDepartment.DataSource = deps.ToList();
                ddlDepartment.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //do insert or update
            using (DefaultConnection db = new DefaultConnection())
            {
                Course objC = new Course();

                if (!String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);
                    objC = (from c in db.Courses
                            where c.CourseID == CourseID
                            select c).FirstOrDefault();
                }

                //populate the course from the input form
                objC.Title = txtTitle.Text;
                objC.Credits = Convert.ToInt32(txtCredits.Text);
                objC.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

                if (String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    //add
                    db.Courses.Add(objC);
                }

                //save and redirect
                db.SaveChanges();
                Response.Redirect("courses.aspx");
            }
        }

        protected void grdStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);
            using (DefaultConnection db = new DefaultConnection())
            {
                Enrollment objE = (from en in db.Enrollments
                                   where en.CourseID == CourseID
                                   select en).FirstOrDefault();

                //process the deletion
                db.Enrollments.Remove(objE);
                db.SaveChanges();

                //repopulate the page
                GetCourse();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                //get values needed
                Int32 StudentID = Convert.ToInt32(ddlStudent.SelectedValue);
                Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);
                //populate new enrollment object
                Enrollment objE = new Enrollment();
                objE.StudentID = StudentID;
                objE.CourseID = CourseID;
                //save
                db.Enrollments.Add(objE);
                db.SaveChanges();
                //refresh
                GetCourse();
            }
        }
    }
}