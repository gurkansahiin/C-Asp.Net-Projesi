<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sans.aspx.cs" Inherits="takimdeneme.sans" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Maç Simülasyonu</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
        }

        .container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            background-color: #ffffff;
            border: 1px solid #cccccc;
            border-radius: 5px;
        }

        .team {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 10px;
            padding: 10px;
            background-color: #e6e6e6;
            border-radius: 3px;
        }

        .team .name {
            font-weight: bold;
        }

        .team .score {
            font-size: 24px;
        }

        .status {
            text-align: center;
            font-size: 20px;
            font-weight: bold;
            margin-top: 20px;
        }

        .counter {
            text-align: center;
            font-size: 48px;
            font-weight: bold;
            margin-top: 50px;
        }

        .button-container {
            text-align: center;
            margin-top: 20px;
        }

        .event-container {
            margin-top: 20px;
            padding: 10px;
            background-color: #f9f9f9;
            border: 1px solid #cccccc;
            border-radius: 3px;
        }

        .event {
            margin-bottom: 5px;
        }

        .event .time {
            font-weight: bold;
        }

        .event .description {
            margin-left: 10px;
        }

        .player-list {
            display: flex;
            justify-content: space-between;
        }

        .player-list .team-players {
            flex: 1;
            padding: 10px;
            background-color: #f2f2f2;
            border: 1px solid #cccccc;
            border-radius: 3px;
        }

        .player-list .team-players ul {
            padding: 0;
            margin: 0;
            list-style-type: none;
        }

        .player-list .team-players li {
            margin-bottom: 5px;
        }
    </style>
    <script>
        var matchTimer;
        var elapsedTime = 0;
        var matchDuration = 90;
        var events = [];

        function startMatch() {
            document.getElementById("matchStatus").innerHTML = "Maç Devam Ediyor";
            document.getElementById("timer").innerHTML = elapsedTime;

            matchTimer = setInterval(updateTimer, 1000);
        }

        function updateTimer() {
            elapsedTime++;

            if (elapsedTime > matchDuration) {
                clearInterval(matchTimer);
                document.getElementById("matchStatus").innerHTML = "Maç Sonu";
            }

            document.getElementById("timer").innerHTML = elapsedTime;

            var homeScore = parseInt(document.getElementById("homeScore").innerHTML);
            var awayScore = parseInt(document.getElementById("awayScore").innerHTML);

            if (Math.random() < 0.1) {
                var scorer = "Ev Sahibi Takım";
                homeScore++;
            } else if (Math.random() < 0.1) {
                var scorer = "Deplasman Takım";
                awayScore++;
            }

            if (scorer) {
                var eventTime = elapsedTime;
                var eventDescription = scorer + " gol attı!";
                events.push({ time: eventTime, description: eventDescription });
                updateEventList();
            }

            document.getElementById("homeScore").innerHTML = homeScore;
            document.getElementById("awayScore").innerHTML = awayScore;
        }

        function updateEventList() {
            var eventContainer = document.getElementById("events");
            eventContainer.innerHTML = "";

            for (var i = 0; i < events.length; i++) {
                var event = events[i];
                var eventElement = document.createElement("div");
                eventElement.className = "event";
                eventElement.innerHTML = "<span class='time'>" + event.time + "'</span><span class='description'>" + event.description + "</span>";
                eventContainer.appendChild(eventElement);
            }
        }
    </script>
</head>
<body>
  <form id="form1" runat="server">
    <div class="container">
        <div class="team">
            <asp:Label ID="evSahibiTakimLabel" runat="server" CssClass="name" Text="Ev Sahibi Takım"></asp:Label>
            <span class="score" id="homeScore">0</span>
        </div>
        <div class="team">
            <asp:Label ID="deplasmanTakimLabel" runat="server" CssClass="name" Text="Deplasman Takım"></asp:Label>
            <span class="score" id="awayScore">0</span>
        </div>
        <div class="player-list">
            <div class="team-players" id="homePlayers">
                <h3>Ev Sahibi Takım Oyuncuları</h3>
                <asp:GridView ID="gridviewHomePlayers" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="No" HeaderText="No" />
                        <asp:BoundField DataField="İsim" HeaderText="İsim" />
                        <asp:BoundField DataField="Pozisyon" HeaderText="Pozisyon" />
                        <asp:BoundField DataField="Durum" HeaderText="Durum" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="team-players" id="awayPlayers">
                <h3>Deplasman Takım Oyuncuları</h3>
                <asp:GridView ID="gridviewAwayPlayers" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="No" HeaderText="No" />
                        <asp:BoundField DataField="İsim" HeaderText="İsim" />
                        <asp:BoundField DataField="Pozisyon" HeaderText="Pozisyon" />
                        <asp:BoundField DataField="Durum" HeaderText="Durum" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="status" id="matchStatus">Maç Başlamadı</div>
        <div class="counter" id="timer">0</div>
        <div class="button-container">
            <asp:Button ID="btnStartMatch" runat="server" Text="Maçı Başlat" />
        </div>
        <div class="event-container">
            <div id="events" runat="server"></div>
        </div>
    </div>
</form>

    <script>
        document.getElementById("btnStartMatch").addEventListener("click", function (e) {
            e.preventDefault();
            startMatch();
        });
    </script>
</body>
</html>
