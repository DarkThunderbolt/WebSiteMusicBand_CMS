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
$("#btnUploadTrack").on('click', function () {

    var formdata = new FormData();
    formdata.append('id', trackId);
    var files = $("#trackUploadFile").get(0).files;
    if (files.length > 0) {
        formdata.append('file', files[0]);
    }
    var ajaxRequest = $.ajax({
        type: "POST",
        url: "/Albums/UploadTrack/",
        cache: false,
        processData: false,
        contentType: false,
        data: formdata,
        success: function (response) {
            if (response != null && response.success) {
                alert(response.responseText);
            } else {
                // DoSomethingElse()
                alert(response.responseText);
            }
        },
        error: function (response) {
            alert("error!");  // 
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
    $("#nameOfTrack").text(item.Position+ ") " + item.NameOfTrack);
    trackId = item.TrackId;
    var modal = document.getElementById('myModal');
    modal.style.display = "block"
    $("#btnUploadTrack").prop('disabled', true);;
}