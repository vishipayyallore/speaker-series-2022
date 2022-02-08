const baseUrl = 'http://localhost:4000/api/books';

export async function getAllBooks() {

    return fetch(baseUrl)
        .then(handleResponse)
        .catch(handleError);
}

export async function getBookById(bookId) {

    return fetch(`${baseUrl}/${bookId}`)
        .then(handleResponse)
        .catch(handleError);
}

export async function saveBook(book) {

    return fetch(baseUrl, {
        method: "POST",
        headers: { "content-type": "application/json" },
        body: JSON.stringify({
            ...book
        })
    })
        .then(handleResponse)
        .catch(handleError);
}

export async function editBook(book) {

    return fetch(`${baseUrl}/${book._id}`, {
        method: "PUT",
        headers: { "content-type": "application/json" },
        body: JSON.stringify({
            ...book
        })
    })
        .then(handleResponse)
        .catch(handleError);
}

export async function deleteBookById(bookId) {

    return fetch(`${baseUrl}/${bookId}`, { method: "DELETE" })
        .then(handleResponse)
        .catch(handleError);
}

async function handleResponse(response) {

    if (response.ok) {
        if (response.status === 204) {
            return;
        }
        else {
            return response.json();
        }
    }

    if (response.status !== 200 || response.status !== 201) {
        const error = await response.text();
        throw new Error(error);
    }

    throw new Error("Network response was not ok.");
}

function handleError(error) {
    console.error("API call failed. " + error);
    throw error;
}
