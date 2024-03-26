<script lang="ts">
	import EditEventModal from './Modals/EditEventModal.svelte';
	import Edit from './icons/Edit.svelte';
	import type { EventModel } from '$lib/components/controls/eventsModel';
	import { formatDateString } from '$lib/utils';

	let { event } = $props<{ event: EventModel }>();

	const renderValue = (value: string | null | undefined) => {
		if (value && value.length > 0) {
			return value;
		}

		return 'Значение не задано';
	};

	let dataFilled = $derived(
		event.address &&
			event.address?.length > 0 &&
			event.description &&
			event.description?.length > 0 &&
			event.startAtUtc
	);
</script>

<div
	class="w-full flex flex-col border {dataFilled ? 'border-primary' : 'border-error'} rounded-lg"
>
	<div class="collapse collapse-arrow">
		<input type="checkbox" />
		<div class="collapse-title">
			<div class="flex flex-row justify-between items-center">
				<span class="font-semibold">Основные данные</span>
			</div>
		</div>
		<div class="collapse-content flex flex-col gap-4">
			<div class="grid grid-cols-8 font-sm gap-2">
				<span class="col-span-2">Aдрес:</span>
				<span class="col-span-6 font-thin">{renderValue(event.address)}</span>

				<span class="col-span-2">Время начала:</span>
				<span class="col-span-6 font-thin"
					>{event.startAtUtc ? formatDateString(event.startAtUtc) : 'Значение не задано'}</span
				>

				<span class="col-span-2">Описание:</span>
				<span class="col-span-6 font-thin">{event.description}</span>
			</div>
			<div class="flex flex-col w-full items-start">
				<!-- <EditEventModal bind:event={event}>
					<button class="btn btn-neutral btn-outline btn-xs"
						><Edit size={20} /> Редактировать</button
					>
				</EditEventModal> -->
			</div>
		</div>
	</div>
</div>
