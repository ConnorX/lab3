<%@ Page Title="Department Details" Language="C#" MasterPageFile="~/monday.Master" AutoEventWireup="true" CodeBehind="department.aspx.cs" Inherits="comp2007_lesson9_lab3.department" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Department Details</h1>

    <h5>All fields are required</h5>

    <div class="form-group">
        <label for="txtName" class="col-sm-3">Name:</label>
        <asp:TextBox ID="txtName" runat="server" required="true" MaxLength="50" />
    </div>
    <div class="form-group">
        <label for="txtBudget" class="col-sm-3">Budget:</label>
        <asp:TextBox ID="txtBudget" runat="server" required="true" MaxLength="22" />
        <asp:RangeValidator runat="server" ControlToValidate="txtBudget"
             CssClass="label label-danger" ErrorMessage="Must be between 0 and 10,000,000"
         MinimumValue="0" MaximumValue="10000000" Type="Currency" />
    </div>

    <div class="col-sm-offset-3">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
             OnClick="btnSave_Click" />
    </div>

    <asp:Panel ID="pnlCourses" runat="server" Visible="false">
    <h2>Courses</h2>
    <asp:GridView ID="grdCourses" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover" 
         DataKeyNames="CourseID" OnRowDeleting="grdCourses_RowDeleting">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="Course Code" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Credits" HeaderText="Credits" />
            <asp:CommandField DeleteText="Delete" HeaderText="Delete" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
    </asp:Panel>
</asp:Content>
