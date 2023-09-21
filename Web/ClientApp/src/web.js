export async function fetchPost(route, body) {
  let response = await fetch(route, {
    method: 'POST',
    headers: {
    'Content-Type': 'application/json',
    },
    body: JSON.stringify(body)
  })
  return response;
}

export async function fetchGet(route) {
  let response = await fetch(route, {
    method: 'GET'
  })
  return response;
}
