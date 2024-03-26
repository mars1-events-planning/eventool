<script lang="ts">
	import { graphql } from '$houdini';
	import SuccessPopup from '../SuccessPopup.svelte';
	import LabeledInput from '../controls/LabeledInput.svelte';
	import LabeledTextArea from '../controls/LabeledTextArea.svelte';
	import ValidationErrorsList from '../controls/ValidationErrorsList.svelte';
	import type { EventModel } from '../controls/eventsModel';
	import Modal from './Modal.svelte';

	let { event } = $props<{
		event: EventModel;
	}>();
	let pad = (n: number) => (n % 10 === n ? `0${n}` : `${n}`);

	let title = $state('');
	let description = $state<string>('');
	let address = $state<string>('');
	let startAtDate = $state<string>('');
	let startAtTime = $state<string>('');
	let success = $state<boolean>();

	$effect(() => {
		title = event?.title ?? '';
		description = event?.description ?? '';
		address = event?.address ?? '';
		let startAtUtcDate = event.startAtUtc ? new Date(event.startAtUtc!) : new Date();
		startAtDate = `${startAtUtcDate.getFullYear()}-${pad(startAtUtcDate.getMonth() + 1)}-${pad(startAtUtcDate.getDate())}`;
		startAtTime = `${startAtUtcDate.getHours()}:${startAtUtcDate.getMinutes()}`;
	});

	$inspect(startAtTime);
	$inspect(startAtDate);

	let store = graphql(`
		mutation EditEvent($input: EditEventInput!) {
			editEvent(input: $input) {
				gqlEvent {
					title
					description
					address
					startAtUtc
					changedAtUtc
				}
				errors {
					code: __typename
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

	let validationErrors = $derived(
		$store.data?.editEvent?.errors
			?.filter((error) => error.code === 'ValidationError')
			?.flatMap((error) => ('errors' in error ? error.errors : []))
			?.map((error) => error!) ?? []
	);
</script>

<Modal title="Сохранить основные данные">
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
			labelClass="w-min"
			inputClass="w-full bg-primary/15 col-span-4"
		/>
		<div></div>
		<div class="col-span-4">
			<ValidationErrorsList errors={validationErrors?.filter((x) => x.propertyName === 'Title')} />
		</div>
		<LabeledTextArea
			id="description"
			name="description"
			bind:value={description}
			label="Описание"
			labelClass="w-min"
			inputClass="w-full bg-primary/15 col-span-4"
		/>
		<div></div>
		<div class="col-span-4">
			<ValidationErrorsList
				errors={validationErrors?.filter((x) => x.propertyName === 'Description')}
			/>
		</div>
		<LabeledInput
			id="address"
			name="address"
			bind:value={address}
			label="Адрес"
			type="text"
			labelClass="w-min"
			inputClass="w-full bg-primary/15 col-span-4"
		/>
		<div></div>
		<div class="col-span-4">
			<ValidationErrorsList
				errors={validationErrors?.filter((x) => x.propertyName === 'Address')}
			/>
		</div>
		<LabeledInput
			id="startAtTime"
			name="startAtTime"
			bind:value={startAtTime}
			label="Время начала"
			type="time"
			labelClass="w-min"
			inputClass="w-full bg-primary/15 col-span-4"
		/>
		<LabeledInput
			id="startAtDate"
			name="startAtDate"
			bind:value={startAtDate}
			label="Дата начала"
			type="date"
			labelClass="w-min"
			inputClass="w-full bg-primary/15 col-span-4"
		/>
		<div></div>
		<div class="col-span-4">
			<ValidationErrorsList
				errors={validationErrors?.filter((x) => x.propertyName === 'StartDateTimeUtc')}
			/>
		</div>
		<button
			type="button"
			class="btn col-span-5 btn-primary w-full"
			disabled={title.trim().length === 0}
			onclick={async () => {
				let result = await store.mutate({
					input: {
						title: title,
						description: description,
						address: address,
						eventId: event.id,
						startDateTimeUtc: `${startAtTime} ${startAtDate}`
					}
				});

				if (result.data?.editEvent?.gqlEvent) {
					success = true;
					return;
				}
				success = false;
			}}
		>
			Сохранить
		</button>
		{#if success}
			<div class="flex flex-col col-span-5 items-center justify-center">
				<SuccessPopup />
			</div>
		{/if}
	</form>
</Modal>
