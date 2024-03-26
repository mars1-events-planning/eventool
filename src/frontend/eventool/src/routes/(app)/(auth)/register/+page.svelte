<script lang="ts">
	import { goto } from '$app/navigation';
	import { graphql } from '$houdini';
	import LabeledInput from '$lib/components/controls/LabeledInput.svelte';
	import ValidationErrorsList from '$lib/components/controls/ValidationErrorsList.svelte';

	let fullname = $state('');
	let username = $state('');
	let password = $state('');

	const store = graphql(`
		mutation RegisterOrganizer($input: RegisterOrganizerInput!) {
			registerOrganizer(input: $input) {
				organizer {
					id
					fullname
					username
				}
				errors {
					code: __typename
					... on Error {
						message
					}
					... on ValidationError {
						message
						errors {
							propertyName
							errorMessage
						}
					}
				}
			}
		}
	`);

	let allErrors = $derived($store.data?.registerOrganizer?.errors);
	let validationErrors = $derived(allErrors?.flatMap((x) => x.errors));
</script>

<svelte:head>
	<title>Регистрация</title>
</svelte:head>


<div class="flex flex-col p-5 space-y-5 text-md">
	<form class="flex flex-col space-y-4">
		<header class="flex flex-row justify-center">
			<span class="text-4xl">создать аккаунт</span>
		</header>
		<div class="grid grid-cols-5 gap-4">
			<LabeledInput
				id="fullname"
				name="fullname"
				bind:value={fullname}
				label="Имя"
				type="text"
				labelClass="w-min"
				inputClass="col-span-4 w-full bg-primary/15"
			/>

			<div></div>
			<div class="col-span-4">
				<ValidationErrorsList
					errors={validationErrors?.filter((x) => x?.propertyName === 'FullName')}
				/>
			</div>

			<LabeledInput
				id="login"
				name="login"
				bind:value={username}
				label="Логин"
				type="text"
				labelClass="w-min"
				inputClass="col-span-4 w-full bg-primary/15"
			/>

			<div></div>
			<div class="col-span-4">
				<ValidationErrorsList
					errors={validationErrors?.filter((x) => x?.propertyName == 'Username')}
				/>
			</div>

			<LabeledInput
				id="password"
				name="password"
				bind:value={password}
				label="Пароль"
				type="password"
				labelClass="w-min"
				inputClass="col-span-4 w-full bg-primary/15"
			/>

			<div></div>
			<div class="col-span-4">
				<ValidationErrorsList
					errors={validationErrors?.filter((x) => x?.propertyName == 'Password')}
				/>
			</div>

			<div class="col-span-5 flex flex-row justify-between">
				<button
					type="button"
					class="btn btn-primary text-white rounded-lg w-full"
					on:click={async () => {
						await store.mutate({
							input: {
								fullName: fullname,
								username: username,
								password: password
							}
						});
						
						if(!allErrors) {
							await goto('/login')
						}
					}}
				>
					Создать
				</button>
			</div>
		</div>
	</form>
	<div class="divider"></div>
	<div class="flex flex-row justify-center">
		<p class="font-thin">
			Уже есть аккаунт? <a href="/login" class="link">Войдите.</a>
		</p>
	</div>
</div>
