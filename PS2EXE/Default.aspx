<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PS2EXE.Default1" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }
        function callAlert(msg) {
            alert(msg);
        }

    </script>
    <div class="col-md-10 col-md-offset-1 container">
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
<%--            <asp:Button ID="Button1" runat="server" Text="Create EXE" OnClick="bt_Compile2_Click" />--%>
            <asp:LinkButton ID="bt_Compile" runat="server" CssClass="btn btn-primary" OnClick="bt_Compile_Click">
                <span aria-hidden="true" class="glyphicon glyphicon-send"></span> Create EXE
            </asp:LinkButton>
        </div>
        <br />
        <div class="messagealert row" id="alert_container">
        </div>
    </div>
</asp:Content>
