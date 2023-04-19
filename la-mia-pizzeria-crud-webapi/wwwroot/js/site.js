
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
// CATEGORIE

const loadCategories = () => getCategories().then(renderCategories);

const getCategories = () => axios
    .get("/api/pizza/category")
    .then(res => res.data);

const renderCategories = categories => {
    const selectCategory = document.querySelector("#categoriaId");
    selectCategory.innerHTML += categories.map(categoryOptionComponent).join('');
};

const categoryOptionComponent = category => `<option value=${category.id}>${category.nome}</option>`;

// INGREDIENTI  

const loadIngredients = () => getIngredients().then(renderIngredients);

const getIngredients = () => axios
    .get("/api/pizza/ingridients")
    .then(res => res.data);

const renderIngredients = ingredienti => {
    const ingredientiSelection = document.querySelector("#ingredienti");
    ingredientiSelection.innerHTML = ingredienti.map(ingredientCheckboxComponent).join('');
}

const ingredientCheckboxComponent = ingrediente => `
	<div class="flex gap">
		<input id="${ingrediente.id}" type="checkbox" />
		<label for="${ingrediente.id}">${ingrediente.name}</label>
	</div>`;


// CREATE

const createPizza = pizza => axios
    .post('/api/pizza', pizza)
    .then(() => location.href = '/menu')
    .catch(e => renderErrors(e.response.data.errors));

const initCreateForm = () => {
    const form = document.querySelector("#create-form");

    form.addEventListener("submit", e => {
        e.preventDefault();

        const pizza = getPizzaFromForm(form);
        createPizza(pizza);
    });
};

const getPizzaFromForm = form => {
    const nome = form.querySelector('#nome').value;
    const descrizione = form.querySelector('#descrizione').value;
    const foto = form.querySelector('#foto').value;
    const prezzo = form.querySelector('#prezzo').value;
    const categoriaId = form.querySelector('#categoriaId').value;

    return {
        nome,
        descrizione,
        foto,
        prezzo,
        categoriaId
    }
}

const renderErrors = errors => {
    const nomeErrors = document.querySelector('#nome-error');
    const descrizioneErrors = document.querySelector('#descrizione-error');
    const fotoErrors = document.querySelector('#foto-error');
    const prezzoErrors = document.querySelector('#prezzo-error');
    const categoriaIdErrors = document.querySelector('#categoriaId-error');

    nomeErrors.innerText = errors.Nome?.join('\n') ?? '';
    descrizioneErrors.innerText = errors.Descrizione?.join('\n') ?? '';
    fotoErrors.innerText = errors.Foto?.join('\n') ?? '';
    prezzoErrors.innerText = errors.Prezzo?.join('\n') ?? '';
    categoriaIdErrors.innerText = errors.CategoriaId?.join('\n') ?? '';
}

// EDIT

const getPizza = id => axios
    .get(`/api/pizza/${id}`)
    .then(res => res.data);
                        

// DELETE

const deletePizza = id => axios
    .delete(`/api/pizza/${id}`)
    .then(() => location.href = '/menu');