<script lang="ts">
	import { goto } from '$app/navigation';
	import { graphql } from '$houdini';
	import LabeledInput from '$lib/components/controls/LabeledInput.svelte';

	let username = $state('');
	let password = $state('');

	let store = graphql(`
		mutation Login($input: LoginInput!) {
			login(input: $input) {
				gqlToken {
					token
				}
				errors {
					... on Error {
						message
					}
				}
			}
		}
	`);

	let errors = $derived($store.data?.login?.errors);
</script>

<svelte:head>
	<title>Вход</title>
</svelte:head>


<div class="flex flex-col p-5 space-y-5 text-md">
	<form class="flex flex-col space-y-4">
		<header class="flex flex-row justify-center">
			<span class="text-4xl">вход в аккаунт</span>
		</header>
		<div class="grid grid-cols-5 gap-4">
			<LabeledInput
				id="login"
				name="login"
				bind:value={username}
				label="Логин"
				type="text"
				labelClass="w-min"
				inputClass="col-span-4 w-full bg-primary/15"
			/>

			<LabeledInput
				id="password"
				name="password"
				bind:value={password}
				label="Пароль"
				type="password"
				labelClass="w-min"
				inputClass="col-span-4 w-full bg-primary/15"
			/>

			<div class="col-span-5 flex flex-row justify-between">
				<button
					type="button"
					class="btn btn-primary text-white rounded-lg w-full"
					on:click={async () => {
						await store.mutate({
							input: {
								username,
								password
							}
						});

						if ($store.data?.login?.gqlToken?.token) {
							let token = $store.data?.login?.gqlToken?.token
							localStorage.setItem('auth-token', token);
							await goto('/list')
						}
					}}
				>
					Войти
				</button>
			</div>
			<div class="col-span-5 text-sm text-red-500">
				{#if errors}
					<div class="divider">Ошибка</div>
					<ol>
						{#each errors as error}
							<li>{error.message}</li>
						{/each}
					</ol>
				{/if}
			</div>
		</div>
	</form>
	<div class="divider"></div>
	<div class="flex flex-row justify-center">
		<p class="font-thin">
			Еще нет аккаунта? <a href="/register" class="link">Зарегистрируйтесь.</a>
		</p>
	</div>
</div>
