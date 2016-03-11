<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoterParticipation.aspx.cs" Inherits="precinctcaptain.myvoters.VoterParticipation" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
    <asp:Chart ID="participationchart" runat="server" RenderType="BinaryStreaming" 
    Height="150px" Width="300px">
        <Titles>
            <asp:Title Name="charttitle" Text="Voter Participation" >
            </asp:Title>
        </Titles>
        <Legends>
            <asp:Legend 
                IsTextAutoFit="true" 
                Name="Default" 
                Font="Trebuchet MS, 8.25pt, style=Bold"
                BackColor="LightGray"
                BackGradientStyle="DiagonalRight"
                ></asp:Legend>
        </Legends>
        <Series>
            <asp:Series Name="voterseries" ChartType="Pie" ChartArea="votingarea" IsValueShownAsLabel="true" Palette="SemiTransparent">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="votingarea">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>

