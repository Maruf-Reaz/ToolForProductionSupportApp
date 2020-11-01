
//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    connectionStart();
    function getNotification() {
        
        $.ajax({
            type: "GET",
            dataType: 'json',
            url: "/Notification/GetNotification",
            cache: false,
            success: function (result) {
                console.log(result);
               
                if (result.length>=5) {
                    document.getElementById("noti_count").textContent = "5+";
                    document.getElementById("notinfication-number").textContent = "5+";
                    
                }
                else {
                    document.getElementById("noti_count").textContent = result.length;
                    document.getElementById("notinfication-number").textContent = result.length;
                }
                $.each(result, function (index, value) {
                   
                    $("#notification-list-viewer").append('<a  href="/Notification/ReadNotification?notificationId=' + value.notificationId + '" class="list-group-item list-group-item-action"><div class="row align-items-center">'
                        + '<div class="col-auto"> <img alt="Image placeholder" src="../img/theme/team-1.jpg"class="avatar rounded-circle">'
                        + ' </div>  <div class="col ml--2">   <div class="d-flex justify-content-between align-items-center">  <div>  <h4 class="mb-0 text-sm">' + value.adderName + '</h4> </div>' +
                        ' <div class="text-right text-muted">  <small>' + value.postingTime + '</small>  </div>  </div>    <p class="text-sm mb-0">' + value.notificationText + '</p >  </div>' +
                        '  </div>  </a>');

                });

                console.log(result);
            },
            error: function (error) {
                console.log(error);
            }
        });

    }

    function getChatNumber(id, target) {
        $.ajax({
            url: "/Chat/GetChatNumber",
            method: "GET",
            
            success: function (result) {

                document.getElementById("chat_count").textContent = result;
            }
        });
    }


    function readNotification(id, target) {
        $.ajax({
            url: "/Notification/ReadNotification",
            method: "GET",
            data: { notificationId: id },
            success: function (result) {
                getNotification();
                $(target).fadeOut('slow');
            },
            error: function (error) {
                console.log(error);
            }
        })
    }


    function connectionStart() {

        var connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build();
        connection.on("Receive", function (user, message) {
            getNotification();
            getChatNumber();
        });
        connection.start().then(function () {

            connection.invoke("Send").catch(function (err) {

                return console.error(err.toString());
            });

        }).catch(function (err) {

            return console.error(err.toString());
        });

    }

    

});
