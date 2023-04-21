function showComment(id, current, max) {
    console.log("click");
    var main = document.getElementById(id);
    console.log(main.style.display);
    if (main.style.display === "block") {
        main.style.display = "none";
    } else {
        main.style.display = "block";
    }
    if (current == max) {
        main.style.textAlign = "center";
        main.innerHTML = "เต็มแล้วจ้า";
    }

}

function showQR(id) {
    console.log("scan");
    var image = document.getElementById(id);
    if (image.style.display === "none") {
        image.style.display = "block";
        image.style.marginLeft = "auto";
        image.style.marginRight = "auto";

    } else {
        image.style.display = "none";
    }

}



