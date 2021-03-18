"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/FlightsHub").build();

connection.on("ReceiveReservation", function (res) {
    var but = "<form type = 'post' asp-controller = 'Agent' \
        asp-action = 'AcceptReservation' \
        asp-route-id = '" +res.reservationId + "' \
        asp-route-seats='" + res.numSeats + "'>\
        <button type='submit'>Accept</button>\
        </form>";
    var reservation  =  "<h3>ReservationID :" + res.reservationId + "</h3>" +
    "Username: "+res.username + 
    " FlightID: " + res.flightID +
    " Number of Seats: " + res.numSeats +
    "<br>" + but;
    var d = document.createElement("div");
    d.innerHTML = reservation;
    var list = document.getElementById("rList");
    list.appendChild(d);
    console.log("reservation: " + reservation);
});

connection.start().catch(function (err) {
    return console.error(err);
});