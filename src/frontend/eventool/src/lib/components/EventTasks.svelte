<script lang="ts">
	import Checklist from '$lib/components/controls/Checklist/Checklist.svelte';
	import RoundProgress from '$lib/components/RoundProgress.svelte';
	import EditChecklistModal from './Modals/EditChecklistModal.svelte';
	import type { EventModel } from './controls/eventsModel';

	let { event } = $props<{event: EventModel;}>();

	let progress = $derived(
		(event.checklists.flatMap((c) => c.items).filter((c) => c.done).length /
        event.checklists.flatMap((c) => c.items).length) * 100
	);

    // 
</script>

<!-- svelte-ignore missing-declaration -->
<div class="w-full flex flex-col border border-primary rounded-lg">
	<div class="collapse collapse-arrow">
		<input type="checkbox" />
		<div class="collapse-title">
			<div class="flex flex-row gap-4 items-center justify-between">
				<span class="font-semibold">Задачи</span>
				<RoundProgress progressPercent={progress} />
			</div>
		</div>

		<div class="collapse-content flex flex-col gap-4">
			<div class="flex flex-col w-full items-center">
				<EditChecklistModal eventId={event.id} checklist={null}>
					<button
						type="button"
						class="btn btn-primary btn-outline text-xs font-bold btn-sm"
					>
						<span class="text-xl">+</span>
						<span>Добавить список задач</span>
					</button>
				</EditChecklistModal>
			</div>
			<div class="flex flex-wrap gap-5 justify-normal">
				{#each event.checklists as _, i}
					<Checklist checklist={event.checklists[i]} />
				{/each}
				<div class="flex flex-col w-full items-center"></div>
			</div>
		</div>
	</div>
</div>
