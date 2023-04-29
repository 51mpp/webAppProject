function showComment(id, current, max, state, bt) {
    console.log("click");
    var main = document.getElementById(id);
    var st = document.getElementById(bt);
    console.log(main);
    console.log(st.innerText);
    if (state === 1) {
        if (main.style.display === "block") {
            main.style.display = "none";
        } else {
            main.style.display = "block";
        }
        if (st.innerText == "กำลังไปส่ง") {
            main.style.textAlign = "center";
            main.innerHTML = "ไม่รับแล้วจ้า";
        }
        if (st.innerText == "ส่งแล้ว") {
            main.style.textAlign = "center";
            main.innerHTML = "ไม่รับแล้วจ้า";
        }
        if (current >= max) {
            main.style.textAlign = "center";
            main.innerHTML = "เต็มแล้วจ้า";
            bt.style.display = "none";
        } else {
            bt.style.display = "block";
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






