<script lang="ts">
	import { graphql } from '$houdini';
	import AvatarPlaceholder from '$lib/components/AvatarPlaceholder.svelte';
	import ChangePassword from '$lib/components/Modals/ChangePassword.svelte';
	import SuccessPopup from '$lib/components/SuccessPopup.svelte';
	import LabeledInput from '$lib/components/controls/LabeledInput.svelte';
	import ValidationErrorsList from '$lib/components/controls/ValidationErrorsList.svelte';

	const currentOrganizerDataStore = graphql(`
		query GetCurrentOrganizerData {
			organizer {
				id
				fullname
				username
			}
		}
	`);

	let editStore = graphql(`
		mutation EditOrganizer($input: EditOrganizerInput!) {
			editOrganizer(input: $input) {
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
						errors {
							propertyName
							errorMessage
						}
					}
				}
			}
		}
	`);

	let initialFullname = $state('');
	let initialUsername = $state('');
	let fullname = $state('');
	let username = $state('');
	let validationErrors = $derived(
		$editStore.data?.editOrganizer?.errors
			?.filter((error) => error.code === 'ValidationError')
			?.flatMap((error) => error.errors)
			?.map((error) => error!) ?? []
	);

	$effect.pre(() => {
		currentOrganizerDataStore.fetch().then((data) => {
			initialFullname = data.data?.organizer?.fullname ?? '';
			initialUsername = data.data?.organizer?.username ?? '';
			fullname = initialFullname;
			username = initialUsername;
		});
	});

	let mutationSuccess = $state(false);
</script>

<div class="flex flex-col gap-2 max-lg:items-center">
	<h1 class="font-semibold text-xl">Профиль</h1>
	<form
		class="flex min-lg:flex-row min-lg:justify-between max-lg:items-center max-lg:flex-col-reverse max-lg:gap-4"
	>
		<div class="grid grid-cols-5 gap-4">
			<div class="grid grid-cols-subgrid col-span-5 items-center">
				<LabeledInput
					id="username"
					name="username"
					bind:value={username}
					label="Логин:"
					type="text"
					labelClass="w-min col-span-1"
					inputClass="w-full col-span-4 bg-primary/15"
				/>
				<div></div>
				<div class="col-span-4 break-all">
					<ValidationErrorsList
						errors={validationErrors?.filter((x) => x.propertyName === 'Username')}
					/>
				</div>
			</div>

			<div class="grid grid-cols-subgrid col-span-5 items-center">
				<LabeledInput
					id="fullname"
					name="fullname"
					bind:value={fullname}
					label="Имя:"
					type="text"
					labelClass="w-min col-span-1"
					inputClass="w-full col-span-4 bg-primary/15"
				/>
				<div></div>
				<div class="col-span-4">
					<ValidationErrorsList
						errors={validationErrors?.filter((x) => x.propertyName === 'Fullname')}
					/>
				</div>
			</div>

			<ChangePassword />

			<button
				type="button"
				class="btn btn-primary btn-sm w-full col-span-5"
				onclick={async () => {
					let result = await editStore.mutate({
						input: {
							fullName: fullname === initialFullname ? undefined : fullname,
							username: username === initialUsername ? undefined : username
						}
					});

					if (result.data?.editOrganizer?.organizer) {
						mutationSuccess = true;
						return;
					}

					mutationSuccess = false;
				}}
			>
				Сохранить
			</button>
		</div>
		<div class="flex-auto flex flex-col items-end gap-2">
			<AvatarPlaceholder avatarClasses="rounded-lg p-2 aspect-square max-w-48 max-h-48 w-full">
				<span class="text-4xl">{initialFullname.slice(0, 2).toUpperCase()}</span>
			</AvatarPlaceholder>
			<button class="btn btn-neutral btn-outline btn-sm max-w-48 w-full">Сменить фото</button>
		</div>
	</form>
</div>
{#if mutationSuccess}
	<SuccessPopup />
{/if}
