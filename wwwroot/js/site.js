document.getElementById("ham-btn").addEventListener("click", () => {
    
});


const validateSignin = () => {
    let isValid = true;
    let $form = document.getElementById("form-login");

    let spansList = document.getElementsByClassName("val-error");

    for(let span of spansList){
        span.innerText = "";
    }

    if (document.getElementById("nombre").value === ""){
        document.getElementById("nombre-error").innerText = "Debe de rellenar el campo nombre";
        isValid = false;
    }
    
    if (document.getElementById("matricula").value === ""){
        document.getElementById("matricula-error").innerText = "Debe de rellenar el campo matricula";
        isValid = false;
    }
       
    if (document.getElementById("password-1").value === ""){
        document.getElementById("password-1-error").innerText = "Debe de rellenar el campo contraseña";
        isValid = false;
    }

    if (document.getElementById("password-2").value !== document.getElementById("password-1").value){
        document.getElementById("password-2-error").innerText = "Las dos contraseñas deben de coincidir";
        isValid = false;
    }

    if (isValid) $form.submit();  
};

const validateLogin = () => {
    let isValid = true;
    let $form = document.getElementById("form-login");

    let spansList = document.getElementsByClassName("val-error");

    for (let span of spansList) {
        span.innerText = "";
    }

    if (document.getElementById("input-name").value === ""){
        document.getElementById("input-name-error").innerText = "Debe de rellenar el campo nombre";
        isValid = false;
    }

    if (document.getElementById("input-password").value === ""){
        document.getElementById("input-password-error").innerText = "Debe de rellenar el campo contraseña";
        isValid = false;
    }

    if (isValid) $form.submit();
};

const validateCreateHomework = () => {
    let isValid = true;
    let $form = document.getElementById("form-create-hw");

    let spansList = document.getElementsByClassName("val-error");

    for (let span of spansList){
        span.innerText = "";
    }

    if (document.getElementById("nombre").value === ""){
        document.getElementById("nombre-error").innerText = "Debes de rellenar el campo nombre";
        isValid = false;
    }

    console.log(document.getElementById("materia").value) 
    if (document.getElementById("materia").value === "0"){
        document.getElementById("materia-error").innerText = "Debes de seleccionar una materia";
        isValid = false;
    }

    if (document.getElementById("descripcion").value === ""){
        document.getElementById("error-descripcion").innerText = "Debes de escribir una descripción para la tarea";
        isValid = false;
    }

    if (document.getElementById("fecha-limite").value === null){
        document.getElementById("error-date").innerText = "Debes de seleccionar una fecha";
        isValid = false;
    }

    if (isValid) {
        $form.submit();
    }
};

const hideInputs = () => {
    let hide = [];
    let unhide = [];

    let label = document.getElementById("nombre");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);
    
    label = document.getElementById("input-nombre");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);

    label = document.getElementById("materia");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);
    
    label = document.getElementById("materia-select");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);
    
    label = document.getElementById("descripcion");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);
    
    label = document.getElementById("input-tarea");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);
    
    label = document.getElementById("text-fecha-limite");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);
    
    label = document.getElementById("fecha-limite");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);
    
    label = document.getElementById("btn-edit");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);
    
    label = document.getElementById("btn-cancell");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);
    
    label = document.getElementById("btn-update");
    if (label.classList.contains("unhide"))
        unhide.push(label);
    else
        hide.push(label);
    
    for (const etiqueta of hide) {
        etiqueta.classList.remove("hide");
        etiqueta.classList.add("unhide");
    }

    for (const etiqueta of unhide) {
        etiqueta.classList.remove("unhide");
        etiqueta.classList.add("hide");
    }
};

const hideFormMatEdit = (id) => {
    let $container = document.getElementById("edit-container");

    if ($container.classList.contains("hide")){
        $container.classList.remove("hide");
        $container.classList.add("unhide");
    }
    else{
        $container.classList.remove("unhide");
        $container.classList.add("hide");
    }

    document.getElementById("id-edit").value = id;
};

const hideFormMatCreate = () => {
    let $container = document.getElementById("create-container");

    if ($container.classList.contains("hide")){
        $container.classList.remove("hide");
        $container.classList.add("unhide");
    }
    else{
        $container.classList.remove("unhide");
        $container.classList.add("hide");
    }
};

const subjectCreate = () => {
    let isValid = true;
    let $form = document.getElementById("fm-create-mat");
    let $input = document.getElementById("create-name");
    let $span = document.getElementById("create-error");

    if ($input.value === ""){
        $span.innerText = "Debe de rellenar el campo nombre";
        isValid = false;
    }

    setTimeout(() => {
        $span.innerText = "";
    }, 5000);

    if (isValid){
        $form.submit();
    }
};

const subjectEdit = () => {
    let isValid = true;
    let $form = document.getElementById("fm-edit-mat");
    let $input = document.getElementById("edit-name");
    let $span = document.getElementById("edit-error");


    if ($input.value === ""){
        $span.innerText = "Debe de rellenar el campo nombre";
        isValid = false;
    }

    setTimeout(() => {
        $span.innerText = "";
    }, 5000);

    if (isValid){
        $form.submit();
    }
};

const showFormUpdateSchedule = (id) => {
    document.getElementById("id-edit").value = id;
    document.getElementById("id-delete").value = id;
    document.getElementById("form-schedule-container").classList.remove("hide");
};

const hideFormUpdateSchedule = () => {
    document.getElementById("form-schedule-container").classList.add("hide");
};

const validateUpdateSchedule = () => {
    let isValid = true;
    let $form = document.getElementById("form-edit-schedule");

    if (document.getElementById("id-edit").value === ""){
        isValid = false;
    }

    if (document.getElementById("edit-name").value === "0"){
        isValid = false;
        document.getElementById("edit-name-error").value = "Debes de seleccionar una materia";
    }

    if (isValid) $form.submit();
};

const deleteSchedule = () => document.getElementById("form-schedule-delete").submit();
