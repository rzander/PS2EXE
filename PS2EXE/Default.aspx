<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PS2EXE.Default1" validateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-10 col-md-offset-1 container">
<%--        <div class="jumbotron">
            <p>test</p>
            <sup>10.Nov.2016</sup>
        </div>--%>
        <div class="row">

            <div class="col-md-12 form-group">
                <label>PowerShell Code:</label>
                <asp:TextBox ID="tb_PSSCript" runat="server" class="form-control" required="required" placeholder="Paste your PowerShell script here..." Rows="20" TextMode="MultiLine" BackColor="#012456" ForeColor="White" Font-Names="Helvetica" Font-Bold="False"></asp:TextBox>
            </div>
            <div class="col-md-12 form-group">
                <label>Filename:</label>
                <asp:TextBox ID="tb_Filename" runat="server" class="form-control" required="required" placeholder="FileName.exe" TextMode="SingleLine"></asp:TextBox>
            </div>
        </div>
        <br />
        <div>
            <asp:LinkButton ID="bt_Compile" runat="server" CssClass="btn btn-primary" OnClick="bt_Compile_Click">
                <span aria-hidden="true" class="glyphicon glyphicon-send"></span> Create EXE
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
