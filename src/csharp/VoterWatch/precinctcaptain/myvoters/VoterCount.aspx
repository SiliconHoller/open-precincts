<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VoterCount.aspx.cs" Inherits="precinctcaptain.myvoters.VoterCount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .selectrow 
        {
            display:inline-block;
            margin-left:1em;
            margin-right:1.5em;
        }
        
        tr.hoverrow 
        {
            background-color:White;
        }
        
        tr:hover 
        {
            background-color:lightyellow;   
        }
        
        .fbutton 
        {
            display:inline-block;
            font-weight: bold;
            background-color:#EEEEFF;
            cursor: pointer;
        }
    </style>

    <script type="text/javascript">
        var pagenum = 0;
        var pagesize = 50;

        function getVotingLedger() {
            var distid = +$("#districtselect option:selected").val();
            var tallyid = +$("#tallyselect option:selected").val();
            var partyid = +$("#partyselect option:selected").val();

            if (distid == 0 || tallyid == 0) return;
            var geturl = partyid == 0 ? "Ledger.svc/script/getVotingLedger" : "Ledger.svc/script/getPartyVotingLedger";
            var gparams = "take=" + pagesize + "&skip=" + (pagenum * pagesize) + "&distid=" + distid + "&tallyid=" + tallyid + (partyid != 0 ? "&partyid=" + partyid : "");
            $("#vlist").html("");
            chartVoterParticipation();
            $.getJSON(geturl, gparams, function (vdata) {
                tablize(vdata);
            });
        }

        function chartVoterParticipation() {
            var did = +$("#districtselect option:selected").val();
            var tid = +$("#tallyselect option:selected").val();
            var pid = +$("#partyselect option:selected").val();
            var params = "districtid=" + did + "&tallyid=" + tid + ((pid == 0) ? "" : "&affid=" + pid);
            $("#participation").html("<img src='VoterParticipation.aspx?" + params + "' alt='Voter Participation' />");
        }


        function tablize(varr) {
            var vtbl = $("#vlist");
            vtbl.html("");
            if (!varr || varr.length == 0) {
                vtbl.html("<tr><td colspan='5'>No more voters assigned to this particular district</td></tr>");
            } else {
                var trows = "";
                for (var i = 0; i < varr.length; i++) {
                    var v = varr[i];
                    trows += "<tr class='hoverrow'>";
                    trows += "<td>" + v.stateid + "</td>";
                    trows += "<td>" + v.countyid + "</td>";
                    trows += "<td><a href='VoterDetail.aspx?voterid=" + v.voterid + "'>" + v.lastname + ", " + v.firstname + "</a></td>";
                    //check for actual vote in this election
                    var vcell = "";
                    if (v.votercountid == 0) {
                        //setup voting button
                        vcell += "<td>";
                        vcell += "<div class='fbutton' voterid='" + v.voterid + "' onclick='recordVote(this);'>";
                        vcell += "Record Vote";
                        vcell += "</div>";
                    }
                    else {
                        vcell = "<td>Voted</td>";
                    }
                    trows += vcell;
                    trows += "</tr>";
                }
                vtbl.html(trows);
            }
        }

        function recordVote(vbutton) {
            var tid = +$("#tallyselect option:selected").val();
            var mid = +$("#methodselect option:selected").val();
            var vid = +($(vbutton).attr("voterid"));
            var jdata = "{\"voterid\" : " + vid + ",\"tallyid\" : " + tid + ",\"vmethod\" : " + mid + "}";
            var parelem = $(vbutton).parent();
            smallElementBusy(parelem);
            $.ajax({
                type: "POST",
                url: "Ledger.svc/script/addVoterTally",
                data: jdata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (addbool) {
                    chartVoterParticipation();
                    if (addbool) {
                        parelem.html("Voted");
                    } else {
                        //failed to add for some reason--display accordingly
                        parelem.html("Recording of vote failed.");
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display:inline-block;width:300px;height:150px;float:left;" id="participation"></div>
    <div style="display:inline-block;">
        <div class="selectrow">
            District:<asp:DropDownList ID="districtselect" runat="server" ClientIDMode="Static" onchange="getVotingLedger();" />
        </div>
        <div class="selectrow">
            Vote: <asp:DropDownList ID="tallyselect" runat="server" ClientIDMode="Static" onchange="getVotingLedger();" />
        </div>
    </div>
    <div style="display:inline-block;">
        <div class="selectrow">
            Party: <asp:DropDownList ID="partyselect" runat="server" ClientIDMode="Static" onchange="getVotingLedger();" />
        </div>
        <div class="selectrow">
            Voting Method: <asp:DropDownList ID="methodselect" runat="server" ClientIDMode="Static" />
        </div>
    </div>
    <table style="width:95%;">
        <thead>
            <tr>
                <th>State Voter ID</th>
                <th>County Voter ID</th>
                <th>Voter Name</th>
                <th>Vote</th>
            </tr>
        </thead>
        <tbody id="vlist">
        
        </tbody>
    </table>

</asp:Content>
