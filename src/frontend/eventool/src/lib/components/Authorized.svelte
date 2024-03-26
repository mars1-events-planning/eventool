<script lang="ts">
	import { goto } from '$app/navigation';
	import Preloader from '$lib/components/icons/Preloader.svelte';
	import { graphql } from '$houdini';

	const authorizedStore = graphql(`
		query IsAuthorized {
			authorized
		}
	`);

	let authorized = $state(false);

	let p = async () => {
		let data = await authorizedStore.fetch();

		if (data.data?.authorized === true) {
			authorized = true;
			return;
		}

		authorized = false;
		await goto('/error');
	};
</script>

{#await p()}
	<Preloader />
{:then}
	{#if authorized}
		<slot />
	{/if}
{/await}
