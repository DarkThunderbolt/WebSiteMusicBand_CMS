//////////////////////////////////////////////////
var modal = document.getElementById('myModal');
var span = document.getElementsByClassName("close")[0];
span.onclick = function () {
    modal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
//////////////////////////////////////////////////
$("#btnUploadTrackZZZ").on('click', function () {
    var formData = new FormData();
    formData.append("file", $("#trackUploadFil")[0].files[0])
    var fileUpload = $("#trackUploadFile").get(0);
    var files = fileUpload.files;

    $.ajax({
        async: true,
        type: "POST",
        url: "/Albums/UploadTrack/" + trackId,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        data: fileData,
        success: function (result) {
            alert(result);
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
});

//////////////////////////////////////////////////
$("#trackUploadFile").on('change', function () {
    //Get count of selected files
    var countFiles = $(this)[0].files.length;
    var imgPath = $(this)[0].value;
    var extn = imgPath.substring(imgPath.lastIndexOf('.') + 1).toLowerCase();
    image_holder.empty();

    if (extn != "mp3") {
        alert("file is not mp3");
        $("#btnUploadTrack").prop('disabled', true);
    } else {
        $("#btnUploadTrack").prop('disabled', false);
    }

});

function uploadTrack(item) {
    $("#nameOfTrack").text(item.Position + ") " + item.NameOfTrack);
    $('#trackId').val(item.TrackId)
    //trackId = item.TrackId;
    var modal = document.getElementById('myModal');
    modal.style.display = "block"
    $("#btnUploadTrack").prop('disabled', true);;
}


