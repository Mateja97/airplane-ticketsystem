"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/FlightsHub").build();

connection.on("ReceiveAcceptedReservation", function (id) {
    var div = document.getElementById(id);
   div.innerHTML = "<span>Accepted</span>";
});
 connection.start().catch(function (err) {
    return console.error(err);
});