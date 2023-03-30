//const botonVerMas = document.getElementById('ver-mas');
/*
    Mira arriba
    No puede ser llamado por el id porque todos tendrian el mismo y van a haber varios botones de ver más, aparte necesitamos mandar el id de la materia 
    por lo que haré una función que recibira como parametro el id y todos los botones de ver más la llamaran al hacer click en ellos.
    Mira la primera funcion
*/
const contenedorDifuminado = document.getElementById('contenedor-difuminado');
const contenedorTareas = document.getElementById('contenedor-tareas');

const verMas = (id) => {
    const $nombre = document.getElementById("nombre");
    const $materia = document.getElementById("materia");
    const $descripcion = document.getElementById("descripcion");
    const $fecha = document.getElementById("text-fecha-limite");

    $nombre.value = document.getElementById(`nombre-${id}`).innerText;
    $materia.value = document.getElementById(`materia-${id}`).innerText;
    $descripcion.value = document.getElementById(`descripcion-${id}`).innerText;
    $fecha.value = document.getElementById(`fecha-${id}`).innerText;

    document.getElementById("update-id").value = id;

    contenedorDifuminado.classList.add("visible");
    contenedorTareas.classList.add("visible");
};

contenedorTareas.addEventListener('click', function (e) {
    e.stopPropagation();
});

contenedorDifuminado.addEventListener('click', function () {
    contenedorDifuminado.classList.remove('visible');
    contenedorTareas.classList.remove('visible');
});