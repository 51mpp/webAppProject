export const addNewPost =(color)=>
{
    var mainC = document.getElementById("CC");
    var newdiv=document.createElement("div");
    newdiv.innerText="Someone";
    newdiv.style.cssText = 'height: 100px;';
    newdiv.style.marginBottom = "30px"
    newdiv.style.boxShadow ="7px 10px 15px LightGray"
    newdiv.style.backgroundColor = color;
    newdiv.className = "col-12 row"
    newdiv.style.padding = "5px"
    newdiv.style.borderRadius = "10px"
    mainC.appendChild(newdiv);
}

export const colorControl =()=>{
}
