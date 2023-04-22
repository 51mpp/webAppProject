function showComment(id, current, max, state) {
    console.log("click");
    var main = document.getElementById(id);
    console.log(main);
    console.log(main.style.display);
    if (state === 1) {
        if (main.style.display === "block") {
            main.style.display = "none";
        } else {
            main.style.display = "block";
        }
        if (current >= max) {
            main.style.textAlign = "center";
            main.innerHTML = "เต็มแล้วจ้า";
        }
    }
}

function showQR(id, state) {
    console.log("scan");
    var image = document.getElementById(id);
    if (state === 2) {
        if (image.style.display === "none") {
            image.style.display = "block";
            image.style.marginLeft = "auto";
            image.style.marginRight = "auto";

        } else {
            image.style.display = "none";
        }
    }
}



