
// PIZZE

const loadPizze = filter => getPizzas(filter).then(renderPizzas);


const getPizzas = nome => axios
    .get('api/pizza', nome ? { params: { nome } } : {})
    .then(res => res.data)
    .catch(e => console.log(e));


const renderPizzas = pizze => {
    const table = document.querySelector('table');
    const pizzeTable = document.querySelector('#pizze-table');
    const noPizze = document.querySelector('#no-pizza');
    const filter = document.querySelector("#filter");


    if (pizze && pizze.lenght > 0) {
        noPizze.classList.remove('d-none');
        table.classList.add('d-none');
        filter.classList.add('d-none');
    } else {
        noPizze.classList.add('d-none');
        table.classList.remove('d-none');
    }

    pizzeTable.innerHTML = pizze.map(pizzaComponent).join('');
};

const pizzaComponent = pizza => `
        <tr class="text-center align-middle">
        <th scope="row" class="d-flex justify-content-center">
        <a class="btn btn-secondary w-50" href="/pizza/detail/${pizza.id}">${pizza.nome}</a>
        </th>
        <td>${pizza.prezzo}€</td>
        </tr>
    `

const initFilter = () => {
    const filter = document.querySelector("#filter");
    filter.addEventListener("input", (e) => loadPizze(e.target.value))
};
