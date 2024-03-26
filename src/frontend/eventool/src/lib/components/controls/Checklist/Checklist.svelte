<script lang="ts">
	import RoundProgress from "$lib/components/RoundProgress.svelte";
	import type { Checklist } from "../eventsModel";

	let { checklist } = $props<{ checklist: Checklist}>();

	$inspect(checklist)

	let progress = $derived((checklist.items.filter((x) => x.done).length / checklist.items.length) * 100);
</script>

<div class="flex flex-col border w-fit p-4 rounded-md h-min">
	<div class="flex flex-row justify-between items-center gap-5">
		<h3 class="font-semibold">{checklist.title}</h3>
		<RoundProgress progressPercent={progress} />
	</div>
	<div class="divider m-0"></div>
	<div class="flex flex-col gap-2">
		{#each checklist.items as item, i}
			<div class="flex flex-row gap-4">
				<input type="checkbox" class="checkbox checkbox-primary" bind:checked={item.done} />
				<span>{item.title}</span>
			</div>
		{/each}
	</div>
</div>
