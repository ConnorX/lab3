<%@ Page Title="Students" Language="C#" MasterPageFile="~/Monday.Master" AutoEventWireup="true" CodeBehind="students.aspx.cs" Inherits="comp2007_lesson9_lab3.students" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Students</h1>

    <a href="student.aspx">Add Student</a>

    <div>
        <label for="ddlPageSize">Records Per Page:</label>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" 
            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Value="3" Text="3" />
            <asp:ListItem Value="5" Text="5" />
            <asp:ListItem Value="10" Text="10" />
        </asp:DropDownList>
    </div>
    <asp:GridView ID="grdStudents" runat="server" CssClass="table table-striped table-hover"
        AutoGenerateColumns="false" OnRowDeleting="grdStudents_RowDeleting" DataKeyNames="StudentID" AllowPaging="true"
        OnPageIndexChanging="grdStudents_PageIndexChanging" PageSize="3" AllowSorting="true"
        OnSorting="grdStudents_Sorting" OnRowDataBound="grdStudents_RowDataBound">
        <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="Student ID" SortExpression="StudentID" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField DataField="FirstMidName" HeaderText="First Name" SortExpression="FirstMidName" />
            <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" DataFormatString="{0:MM-dd-yyyy}" SortExpression="EnrollmentDate" />
            <asp:HyperLinkField HeaderText="Edit" Text="Edit" NavigateUrl="student.aspx"
                DataNavigateUrlFields="StudentID" 
                DataNavigateUrlFormatString="student.aspx?StudentID={0}" />
            <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>

</asp:Content>
