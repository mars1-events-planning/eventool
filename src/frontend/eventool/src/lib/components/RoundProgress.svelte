<script lang="ts">
	import CheckMark from './icons/CheckMark.svelte';

	let { progressPercent } = $props<{ progressPercent: number; }>();
	let size = '2rem'

	if (isNaN(progressPercent)) progressPercent = 0;
</script>

<div
	class="radial-progress text-primary bg-primary/20"
	style="--value:{progressPercent}; --size:{size}; --thickness:4px;"
	role="progressbar"
>
	{#if progressPercent === 100}
		<CheckMark size={20} />
	{:else if progressPercent !== 100}
		<span style="font-size: xx-small;">{progressPercent.toFixed(0)}%</span>
	{/if}
</div>
