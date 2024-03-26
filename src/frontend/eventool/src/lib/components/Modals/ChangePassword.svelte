<script lang="ts">
	import { graphql } from '$houdini';
	import LabeledInput from '../controls/LabeledInput.svelte';
	import ValidationErrorsList from '../controls/ValidationErrorsList.svelte';
	import Modal from './Modal.svelte';

	let changePasswordStore = graphql(`
		mutation ChangePassword($input: ChangePasswordInput!) {
			changePassword(input: $input) {
				organizer {
					id
					fullname
					username
				}
				errors {
					... on Error {
						message
					}
                    ... on ValidationError {
                        errors {
                            propertyName
                            errorMessage
                        }
                    }
				}
			}
		}
	`);

	let oldPassword = $state('');
	let newPassword = $state('');

	let validationErrors = $derived(
		$changePasswordStore.data?.changePassword?.errors
			?.filter((error) => error.__typename == 'ValidationError' && 'errors' in error)
            .map((error) => error )
            .flatMap((error) => {
                if ('errors' in error) {
                    return error.errors?.map((error) => error!)!;
                }
                return [];
            })
			?? []
	);

	let wrongPasswordErrors = $derived(
		$changePasswordStore.data?.changePassword?.errors
			?.filter((error) => error.__typename == 'WrongPasswordError')
			.map((error) => error.message) ?? []
	);

	let mutationSuccess = $state(false);
</script>

<Modal title="Смена пароля">
	<div slot="button">Сменить пароль</div>

	<form class="flex flex-col w-full" slot="content">
		<LabeledInput
			id="oldPassword"
			name="oldPassword"
			bind:value={oldPassword}
			label="Старый пароль:"
			type="password"
			labelClass=""
			inputClass="w-full bg-primary/15"
		/>
		{#if wrongPasswordErrors.length > 0}
            {#each wrongPasswordErrors as error}
                <div class="text-error self-center">{error}</div>
            {/each}
		{/if}

		<LabeledInput
			id="newPassword"
			name="newPassword"
			bind:value={newPassword}
			label="Новый пароль:"
			type="password"
			labelClass=""
			inputClass="w-full bg-primary/15"
		/>
		{#if validationErrors.length > 0}
            <ValidationErrorsList errors={validationErrors} />
		{/if}

		<button
			type="button"
			class="mt-4 btn btn-primary btn-sm w-full"
			onclick={async () => {
				let result = await changePasswordStore.mutate({
					input: {
						oldPassword: oldPassword,
						newPassword: newPassword
					}
				});

				if (result.data?.changePassword?.organizer) {
					mutationSuccess = true;
					return;
				}

				mutationSuccess = false;
			}}
		>
			Сохранить
		</button>
		{#if mutationSuccess}
			<div class="flex flex-col items-center justify-center w-full">
				<div class=" text-primary self-center">Пароль успешно изменен</div>
			</div>
		{/if}
	</form>
</Modal>
