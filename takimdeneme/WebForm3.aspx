<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="takimdeneme.WebForm3" %>

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
        .flash {
    background-color: red;
    animation: flashAnimation 1s infinite;
}

@keyframes flashAnimation {
    0% { opacity: 1; }
    50% { opacity: 0; }
    100% { opacity: 1; }
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
            margin-top: 10px;
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


 .player-list .team-players .gridview {
    max-height: 120px; /* İstediğiniz boyutu burada belirleyebilirsiniz */
    overflow-y: auto;

}

 #homePlayers{
       max-height: 400px; 
       overflow-y: auto;
       border-radius:40px;
 }
 #awayPlayers{
       max-height: 400px; 
       overflow-y: auto;
       border-radius:40px;
 }
 
 #timer{
     margin-top:-10px;

 }

    .goal-player {
        font-weight: bold;
        margin-left: 10px;
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
              var bakiyeUpdate = 0;
              var textBoxValue = parseInt('<%= Session["TextBoxValue"] %>');

              function startMatch() {
                  if (elapsedTime >= matchDuration) {
                      resetMatch();
                  }

                  document.getElementById("matchStatus").innerHTML = "Maç Devam Ediyor";
                  document.getElementById("timer").innerHTML = elapsedTime;

                  matchTimer = setInterval(updateTimer, 100);
              }

              function updateTimer() {
                  elapsedTime++;

                  if (elapsedTime > matchDuration) {
                      clearInterval(matchTimer);
                      document.getElementById("matchStatus").innerHTML = "Maç Sonu";

                      var homeScore = parseInt(document.getElementById("homeScore").innerHTML);
                      var awayScore = parseInt(document.getElementById("awayScore").innerHTML);

                      var radioSelection = '<%= Session["RadioSelection"] %>';
                      var textBoxValue = parseInt('<%= Session["TextBoxValue"] %>');
                      var userEmail = '<%= Session["mail"] %>';

                      var matchResult = "";


                      if (homeScore > awayScore) {
                          matchResult = "Ev Sahibi Takım";
                      }
                      else if (homeScore < awayScore) {
                          matchResult = "Deplasman Takım";
                      }
                      else {
                          matchResult = "Berabere";
                      }
                     

                      if (matchResult.toLowerCase() === radioSelection.toLowerCase()) {
                          bakiyeUpdate = textBoxValue;
                      }
                      else {
                          bakiyeUpdate = -textBoxValue ;
                      }


                      var xhr = new XMLHttpRequest();
                      xhr.open("POST", "WebForm3.aspx", true);
                      xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                      xhr.onreadystatechange = function () {
                          if (xhr.readyState === 4 && xhr.status === 200) {
                              // Bakiye güncelleme tamamlandı, gerekli işlemleri yapabilirsiniz
                          }
                      };

                      if (bakiyeUpdate > 0) {
                          data = "bakiyeUpdate=" + encodeURIComponent(bakiyeUpdate) + "&userEmail=" + encodeURIComponent(userEmail);
                      }
                      else {
                          data = "bakiyeUpdate=" + encodeURIComponent(bakiyeUpdate) + "&userEmail=" + encodeURIComponent(userEmail);

                      }
                      


                      xhr.send(data);




                  }   

              
                  document.getElementById("timer").innerHTML = elapsedTime;

                  var homeScore = parseInt(document.getElementById("homeScore").innerHTML);
                  var awayScore = parseInt(document.getElementById("awayScore").innerHTML);

                  if (Math.random() < 0.04 && homeScore < 6) {
                      flashPlayerRow("home");
                      var scorer = "Ev Sahibi Takım Oyuncusu";
                      var assister = "";

                      homeScore++;

                      if (Math.random() < 0.2) {
                          assister = "Ev Sahibi Takım Oyuncusu";
                      }

                      events.push({ time: elapsedTime, type: "goal", team: "Ev Sahibi Takım", scorer: scorer, assister: assister });
                  }

                  if (Math.random() < 0.03 && awayScore < 6) {
                      flashPlayerRow("away");
                      var scorer = "Deplasman Takım Oyuncusu";
                      var assister = "";

                      awayScore++;

                      if (Math.random() < 0.2) {
                          assister = "Deplasman Takım Oyuncusu";
                      }

                      events.push({ time: elapsedTime, type: "goal", team: "Deplasman Takım", scorer: scorer, assister: assister });
                  }

                  document.getElementById("homeScore").innerHTML = homeScore;
                  document.getElementById("awayScore").innerHTML = awayScore;

                  updateEvents();
                  flashPlayerRows();
              }

              function resetMatch() {
                  elapsedTime = 0;
                  document.getElementById("homeScore").innerHTML = "0";
                  document.getElementById("awayScore").innerHTML = "0";
                  document.getElementById("events").innerHTML = "";
                  events = [];
              }

              function checkTimer() {
                  var timerValue = parseInt(document.getElementById("timer").innerHTML);

                  // Timer değeri 91 olduğunda yönlendirme yapın
                  if (timerValue === 91) {
                      window.location.href = "dene.aspx";
                  }
              }
            
              setInterval(checkTimer, 500);

              function updateEvents() {
                  var eventsContainer = document.getElementById("events");
                  eventsContainer.innerHTML = "";

                  for (var i = 0; i < events.length; i++) {
                      var event = events[i];
                      var eventText = "";

                      if (event.type === "goal") {
                          eventText = event.time + "' Gol: " + event.scorer;
                      }

                      var eventElement = document.createElement("div");
                      eventElement.classList.add("event");
                      eventElement.innerHTML = "<span class='time'>" + eventText + "</span>";
                      eventsContainer.appendChild(eventElement);
                  }
              }
              
              

              function flashPlayerRow(team) {
                  var playerRows = [];

                  if (team === "home") {
                      playerRows = document.getElementById("gridviewHomePlayers").querySelectorAll("tr:not(:first-child)");
                  } else if (team === "away") {
                      playerRows = document.getElementById("gridviewAwayPlayers").querySelectorAll("tr:not(:first-child)");
                  }

                  var eligiblePlayers = Array.from(playerRows).filter(function (row) {
                      var position = row.cells[1].innerText;
                      return position !== "K";
                  });

                  if (eligiblePlayers.length > 0) {
                      var randomIndex = Math.floor(Math.random() * eligiblePlayers.length);
                      var randomPlayerRow = eligiblePlayers[randomIndex];

                      // Eğer daha önce gol atan varsa yanma süresini sıfırla
                      var previouslyFlashingRow = document.querySelector(".flash");
                      if (previouslyFlashingRow) {
                          previouslyFlashingRow.classList.remove("flash");
                      }

                      randomPlayerRow.classList.add("flash");

                      setTimeout(function () {
                          randomPlayerRow.classList.remove("flash");
                      }, 1000);
                  }
              }

             
              document.getElementById("btnStartMatch").addEventListener("click", function (e) {
                  e.preventDefault();
                  startMatch();
              });
             

          </script>
</head>
<body>
    <form id="form1" runat="server">
       
                      

             <asp:HiddenField runat="server" ID="hdnHomeScore" />
<asp:HiddenField runat="server" ID="hdnAwayScore" />


        <div class="container">
            <div class="team">
                            
                <asp:Label ID="evSahibiTakimLabel" runat="server" CssClass="name" Text="Ev Sahibi Takım"></asp:Label>
                <span class="score" id="homeScore">0</span>
            </div>
            <asp:Label ID="lbldeneme" runat="server" CssClass="name" Text=""></asp:Label>
             <asp:Label ID="secilendeger" runat="server" CssClass="name" Text=""></asp:Label>
            <div class="team">
                <asp:Label ID="deplasmanTakimLabel" runat="server" CssClass="name" Text="Deplasman Takım"></asp:Label>
                <span class="score" id="awayScore">0</span>
            </div>
            <div class="player-list">
                <div class="team-players" id="homePlayers">
                    <h3>Ev Sahibi Takım Oyuncuları</h3>
                    <asp:GridView ID="gridviewHomePlayers" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                        <Columns>
                            <asp:BoundField DataField="İsim" HeaderText="İsim" />
                            <asp:BoundField DataField="Mevki" HeaderText="Mevki" />
                            <asp:BoundField DataField="Piyasa Değeri" HeaderText="[Piyasa Değeri]" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="team-players" id="awayPlayers">
                    <h3>Deplasman Takım Oyuncuları</h3>
                    <asp:GridView ID="gridviewAwayPlayers" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                        <Columns>
                            <asp:BoundField DataField="İsim" HeaderText="İsim" />
                            <asp:BoundField DataField="Mevki" HeaderText="Mevki" />
                            <asp:BoundField DataField="Piyasa Değeri" HeaderText="[Piyasa Değeri]" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="status" id="matchStatus">Maç Başlamadı</div>
            <div class="counter" id="timer">0</div>
            <div class="button-container">
                <asp:Button ID="btnStartMatch" runat="server" Text="Maçı Başlat" OnClick="btnStartMatch_Click" />
           

            </div>
            <div class="event-container">
                <div id="events" runat="server"></div>
            </div>
        </div>
        <input type="hidden" id="hdnMatchResult" runat="server" />
    </form>
   <script>
       document.getElementById("btnStartMatch").addEventListener("click", function (e) {
           e.preventDefault();
           startMatch();
       });



   </script>
</body>
</html>