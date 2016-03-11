<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistrictDetail.aspx.cs" Inherits="precinctcaptain.myvoters.DistrictDetail" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
    <asp:Chart ID="demogchart" runat="server" RenderType="BinaryStreaming" 
    Height="270px" Width="450px">
        <Titles>
            <asp:Title Name="demotitle" Text="Party Affiliation Breakdown" >
            </asp:Title>
        </Titles>
        <Legends>
            <asp:Legend 
                IsTextAutoFit="false" 
                Name="Default" 
                Font="Trebuchet MS, 8.25pt, style=Bold"
                BackColor="LightGray"
                BackGradientStyle="DiagonalRight"
                ></asp:Legend>
        </Legends>
        <Series>
            <asp:Series Name="partyseries" ChartType="Pie" ChartArea="partyarea" IsValueShownAsLabel="true" Palette="SemiTransparent">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="partyarea">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>

