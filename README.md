Hello! This is my realization of an API, which help to create the following records: contacts, accounts, incidents.

You can give an data in request body to specify request.
The main requests is for Incidents via an address: /api/Incidents.
You can also add new accounts and contacts via next addresses: /api/Accounts and /api/Contacts.

An example of request body:
{
account name,
contact first name,
contact last name,
contact email,
incident description,
}
