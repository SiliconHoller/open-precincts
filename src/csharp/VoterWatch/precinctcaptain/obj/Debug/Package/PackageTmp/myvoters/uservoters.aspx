<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="uservoters.aspx.cs" Inherits="precinctcaptain.myvoters.uservoters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        div.leftlist 
        {
            height:95%;
            width:90%;
        }
        
        #userdistricts 
        {
            width:90%;
            
        }
        
        #voterlist 
        {
            width:90%;
        }
        
        
    </style>

    <script type="text/javascript">
        var pagenum = 0;
        var pagesize = 50;


        $(document).ready(function () {
            loadUserDistricts();
            loadParties();
        });

        function loadUserDistricts() {
            var dlist = $("#userdistricts");
            dlist.html("");
            $.getJSON("VotingDistricts.svc/script/userVotingDistricts", function (darr) {
                var optlist = "";
                for (var i = 0; i < darr.length; i++) {
                    optlist += "<option ";
                    optlist += "value='" + darr[i].districtid + "' ";
                    optlist += "title='" + darr[i].descr + "'>";
                    optlist += darr[i].districttype + " - " + darr[i].identifier + (darr[i].name ? (" "+darr[i].name):"");
                    optlist += "</option>";
                }
                dlist.html(optlist);
            });
        }

        function loadParties() {
            var pselect = $("#partyselect");
            pselect.html("");
            $.getJSON("SystemLists.svc/script/getParties", function (jarr) {
                var optlist = "<option value='0' title='All Voters'>All</option>";
                for (var i=0;i<jarr.length;i++) {
                    optlist += "<option ";
                    optlist += "title='"+jarr[i].optip+"' value='"+jarr[i].optval+"'>";
                    optlist += jarr[i].opttext;
                    optlist += "</option>";
                }
                pselect.html(optlist);
            });
        }

        function loadNextVoterList() {
            pagenum++;
            var did = +$("#userdistricts option:selected").val();
            loadDistrictVoters(did);
        }

        function loadPrevVoterList() {
            if (pagenum > 0) pagenum--;
            var did = +$("#userdistricts option:selected").val();
            loadDistrictVoters();
        }

        function loadChangedVoterList() {
            pagenum = 0;
            var did = +$("#userdistricts option:selected").val();
            loadDistrictVoters(did);
            loadDemoChart(did);
        }

        function loadDemoChart(distid) {
            var chartbox = $("#breakdown");
            chartbox.html("");
            var imgtag = "<img src='DistrictDetail.aspx?districtid=" + distid + "'/>";
            chartbox.html(imgtag);
        }

        function loadDistrictVoters(did) {
            var pid = +$("#partyselect option:selected").val();
            if (pid == 0) pid = "";
            $.getJSON("VoterInfo.svc/script/getDistrictVoters", dvparams(pid, did), function (varr) {
                var vtbl = $("#vlist");
                vtbl.html("");
                if (!varr || varr.length == 0) {
                    //just put in no voters found
                    vtbl.html("<tr><td colspan='4'>No more voters assigned to this particular district</td></tr>");
                } else {
                    var trows = "";
                    for (var i = 0; i < varr.length; i++) {
                        var v = varr[i];
                        trows += "<tr>";
                        trows += "<td>" + v.stateid + "</td>";
                        trows += "<td>" + v.countyid + "</td>";
                        trows += "<td><a href='VoterDetail.aspx?voterid="+v.voterid+"'>" + v.lastname + ", " + v.firstname + "</a></td>";
                        trows += "<td>" + v.yob + "</td>";
                        var dtval;
                        try {
                            var linedate = new Date(parseInt(v.registrationdate.substr(6)));
                            dtval = linedate.toLocaleString();
                        } catch (ex) {
                            dtval = "";
                        }
                        trows += "<td>" + dtval + "</td>";
                        trows += "<td>" + (v.partyaffiliation ? v.partyaffiliation : "") + "</td>";
                        trows += "</tr>";
                    }
                    vtbl.html(trows);
                }
            });
        }

        function dvparams(pid, did) {
            var params = "districtid=" + did;
            params += "&affid=" + pid;
            params += "&skip=" + (pagesize * pagenum);
            params += "&take=" + pagesize;
            return params;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="leftlist">
        <p>Assigned Districts</p>
        <select id="userdistricts" size="10" onchange="loadChangedVoterList();" >
        
        </select>
    </div>
    <div style="margin-top:3em;">
        <div id="breakdown">
            
        </div>
        <div>
            Show affiliation: <select id="partyselect" onchange="loadChangedVoterList();"></select>
        </div>
        <p><span style="cursor:pointer;" onclick="loadPrevVoterList();">&lt;&lt;</span>&nbsp;&nbsp;<span style="cursor:pointer;" onclick="loadNextVoterList();">&gt;&gt;</span></p>
        <table id="voterlist">
            <thead>
                <tr>
                    <th>State Identifier</th>
                    <th>County Identifier</th>
                    <th>Name</th>
                    <th>Year of Birth</th>
                    <th>Registration Date</th>
                    <th>Party Affiliation</th>
                </tr>
            </thead>
            <tbody id="vlist">

            </tbody>
        </table>
    </div>
</asp:Content>
