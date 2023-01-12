let notes = document.querySelectorAll(".note-box"); //список всех записок

let changeActiveNote = function (event) { //функция смены активной записки
    console.log("change");
    for (let item of notes) {
        item.classList.remove("active");
    }
    event.target.closest(".note-box").classList.add("active");
} 

let firstNote = document.querySelector(".note-box"); //выделение первой записки
firstNote.classList.add("active");




for (let note of notes) {
    note.addEventListener("click", changeActiveNote); //добавление функции к каждой записке
}


let updateBtn = document.querySelector(".update-btn");
let deleteBtn = document.querySelector(".delete-btn");
console.log(notes);
console.log(deleteBtn, updateBtn);


