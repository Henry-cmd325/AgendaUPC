const botonVerMas = document.getElementById('ver-mas');
const contenedorDifuminado = document.getElementById('contenedor-difuminado');
const contenedorTareas = document.getElementById('contenedor-tareas');

botonVerMas.addEventListener('click', function () {
    contenedorDifuminado.classList.add('visible');
    contenedorTareas.classList.add('visible');
});

contenedorTareas.addEventListener('click', function (e) {
    e.stopPropagation();
});

contenedorDifuminado.addEventListener('click', function () {
    contenedorDifuminado.classList.remove('visible');
    contenedorTareas.classList.remove('visible');
});