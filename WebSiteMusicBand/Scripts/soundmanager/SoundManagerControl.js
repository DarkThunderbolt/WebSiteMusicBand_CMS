function CreateMusicArray(tracks) {
    var arrayDiv = $("ul.sm2-playlist-bd");
    var elements = [];
    $.each(tracks, function (i, item) {
        var music = $("<li>").append(
            $("<div>").addClass("sm2-row").append(
                $("<div>").addClass("sm2-col sm2-wide").append(
                    $("<a>").addClass("songLink").attr("href", item.TrackLink).append(
                        $("<div>").addClass("songName").text(item.NameOfTrack)
            )   )   )   )
        elements.push(music);
    });
    arrayDiv.append(elements);
}