<script>
	import { goto } from '$app/navigation';
	import { graphql } from '$houdini';
	import LabeledInput from '../controls/LabeledInput.svelte';
	import Create from '../icons/Create.svelte';
	import Modal from './Modal.svelte';

	let title = $state('');

	let store = graphql(`
		mutation CreateEvent($input: CreateEventInput!) {
			createEvent(input: $input) {
				__typename
				gqlEvent {
					id
				}
				errors {
					code: __typename
					... on Error {
						message
					}
				}
			}
		}
	`);
</script>

<Modal title="Создать мероприятие">
	<div slot="button">
		<slot />
	</div>

	<form slot="content" class="grid grid-cols-5 gap-2">
		<LabeledInput
			id="title"
			name="title"
			bind:value={title}
			label="Название"
			type="text"
			labelClass="w-min col-span-5"
			inputClass="w-full bg-primary/15 col-span-4"
		/>
		<button
			type="button"
			class="btn btn-primary w-full"
			disabled={title.trim().length === 0}
			onclick={async () => {
				let result = await store.mutate({
					input: {
						title: title
					}
				});

				if (!result.data?.createEvent?.errors) {
					await goto(`event/${result.data?.createEvent.gqlEvent?.id}/edit`);
				}
			}}
		>
			Создать
		</button>
	</form>
</Modal>
