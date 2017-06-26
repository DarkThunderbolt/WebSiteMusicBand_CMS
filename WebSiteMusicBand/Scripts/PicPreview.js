
$("#fileUpload").on('change', function () {
    //Get count of selected files
    var countFiles = $(this)[0].files.length;

    var imgPath = $(this)[0].value;
    var extn = imgPath.substring(imgPath.lastIndexOf('.') + 1).toLowerCase();
    var image_holder = $("#imgPreview");
    image_holder.empty();

    if (extn == "gif" || extn == "png" || extn == "jpg" || extn == "jpeg") {
        if (typeof (FileReader) != "undefined") {

            //loop for each file selected for uploaded.
            for (var i = 0; i < countFiles; i++) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $("<img />", {
                        "src": e.target.result,
                        "id": "thumb-image"
                    }).appendTo(image_holder);
                }

                image_holder.show();
                reader.readAsDataURL($(this)[0].files[i]);
            }

        } else {
            alert("This browser does not support FileReader.");
        }
    } else {
        alert("Pls select only images");
        if (link != null) {
            image_holder.empty();
            $("<img />", {
                "src": "/" +link,
                "id": "thumb-image"
            }).appendTo(image_holder);

        }
        var $el = $('#fileUpload');
        $el.wrap('<form>').closest('form').get(0).reset();
        $el.unwrap();
    }
});


$("#resetCover").click(function () {

    var $el = $('#fileUpload');
    $el.wrap('<form>').closest('form').get(0).reset();
    $el.unwrap();

    image_holder.empty();
    $("<img />", {
        "src": "/" + link,
        "id": "thumb-image"
    }).appendTo(image_holder);
});


/*
<div class="textCenter">
    <label ck for="file">Upload Image:</label>
    <input id="fileUpload" type="file" name="file" />
    <div id="imgPreview"></div>
    <br />
</div>
*/