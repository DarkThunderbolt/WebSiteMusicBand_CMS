﻿var selectedTrackId;
var elements = [];
var button;
function dropdownMenu(renderPageScripts, userId,id,newPlaylist = 0) {

    $.ajax({
        type: "GET",
        url: "/api/PlaylistAjax/" + userId,
        dataType: "json",
        success: function (data) {
            if (data.length != 0) {
                $.each(data, function (i, item) {
                    var linkToAdd = $("<a>").attr('name', item.PlaylistId).text(item.Title).addClass('addToPlaylist');
                    elements.push(linkToAdd);
                });

                var divs = $("<div>").addClass("dropdown-content").append(elements);
                button = $("<button>").addClass("dropbtn").text("Add to:").append(divs);

                $('body').on('click', 'a.addToPlaylist', function () {
                    if (newPlaylist == 0) {
                        $.ajax({
                            type: "Post",
                            url: "/api/playlisttracksajax/" + selectedTrackId + "?playlistId=" + $(this).attr('name'),
                            success: function (success) {
                                if (success) {
                                    // +
                                    alert("add")
                                } else {
                                    // -
                                    alert("smt was wrong. Try again later.")
                                    alert(response.responseText);
                                }
                            }
                        });
                    }
                    else {
                        $.ajax({
                            type: "Post",
                            url: "/api/PlaylistTracksAjax/" + selectedTrackId + "?oldPlaylistId=" + $(this).attr('name') + "&newPlaylistId" + newPlaylist,
                            success: function (success) {
                                if (success) {
                                    // +
                                    $("#jsGrid_Tracks").jsGrid("loadData");
                                    alert("moved")
                                } else {
                                    // -
                                    alert("smt was wrong. Try again later.")
                                    alert(response.responseText);
                                }
                            }
                        });
                    }

                });

                $('body').on('click', 'button.dropbtn', function () {
                    closeDrop();
                    selectedTrackId = $(this).attr('name');
                    $(this).find(".dropdown-content").toggleClass("show");
                });

                // Close the dropdown menu if the user clicks outside of it
                window.onclick = function (event) {
                    if (!event.target.matches('.dropbtn')) {
                        closeDrop();
                    };
                };
            }
        }
    }).done(function () {
        renderPageScripts(id);
        });
};

function closeDrop() {
    var dropdowns = document.getElementsByClassName("dropdown-content");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
        var openDropdown = dropdowns[i];
        if (openDropdown.classList.contains('show')) {
            openDropdown.classList.remove('show');
        }
    }
}
