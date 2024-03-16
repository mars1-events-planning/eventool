<script>
	import { graphql } from '$houdini';
	import Authorized from '$lib/components/Authorized.svelte';
	import CreateEventModal from '$lib/components/Modals/CreateEventModal.svelte';
	import { formatDateString } from '$lib/utils';
	import Trash from '$lib/components/icons/Trash.svelte';
	import Preloader from '$lib/components/icons/Preloader.svelte';

	const eventsStore = graphql(`
		query PaginatedEvents($page: Int!) {
			events(page: $page) {
				id
				title
				createdAt
				changedAt
			}
		}
	`);

	let pageNumber = $state(1);
	const refetch = async () => {
		await eventsStore.fetch({
			variables: {
				page: pageNumber
			}
		});
	};
	let events = $derived($eventsStore.data?.events ?? []);
	let nextAvailable = $derived(events.length === 15); // pagesize on backend

	$effect.pre(() => {
		refetch();
	});
</script>

<svelte:head>
	<title>Мероприятия</title>
</svelte:head>

<Authorized>
	<div class="divider divider-primary">Список мероприятий</div>
	<div class="flex flex-col gap-2">
		<div class="overflow-x-auto max-h-[70vh] rounded-md border border-base-content/30">
			<table id="custom" class="table table-pin-cols table-pin-rows max-h-36">
				<thead class="">
					<tr class="shadow-md">
						<th>Название</th>
						<th>Последнее редактирование</th>
						<th class="flex flex-row justify-end space-x-2">
							<CreateEventModal />
						</th>
					</tr>
				</thead>
				<tbody>
					{#each events as e}
						{@const createdAt = formatDateString(e.createdAt)}
						{@const changedAt = formatDateString(e.changedAt)}
						<tr>
							<td>
								<a href={`/event/${e.id}/edit`}>
									<div class="flex flex-row space-x-2 items-center font-semibold">
										<div class="avatar placeholder text-neutral-content">
											<div class="bg-primary w-10 rounded-full">
												<img
													src="https://wishpics.ru/site-images/wishpics_ru_3145.jpg"
													alt={e.title.slice(0, 2)}
												/>
											</div>
										</div>
										<div class="flex flex-col gap-1">
											<span class=" font-semibold">{e.title}</span>
											<div class="badge badge-ghost badge-sm">
												Создано: <span class="font-mono font-thin pl-1"> {createdAt}</span>
											</div>
										</div>
									</div>
								</a>
							</td>
							<td class="font-mono"> {changedAt} </td>
							<td class="flex flex-row justify-end">
								<button
									class="btn btn-outline btn-error btn-sm"
									onclick={() => {
										console.log(`mock delete ${e.id}`);
									}}
								>
									<Trash size={20} />
								</button>
							</td>
						</tr>
					{/each}
				</tbody>
			</table>
			{#if events.length === 0}
				{#if $eventsStore.fetching}
					<Preloader />
				{:else}
					<div class="w-full items-center text-center py-2">
						<span class="text-base-content/50">{'Мероприятий пока не создано :)'}</span>
					</div>
				{/if}
			{/if}
		</div>
		<div class="join w-full justify-center">
			<button
				class="join-item btn btn-ghost"
				disabled={pageNumber === 1}
				on:click={() => pageNumber--}
				>«
			</button>
			<button class="join-item btn btn-ghost">{pageNumber}</button>
			<button
				class="join-item btn btn-ghost"
				disabled={!nextAvailable}
				on:click={() => pageNumber++}
				>»
			</button>
		</div>
	</div>
</Authorized>

<style lang="postcss">
	thead tr th {
		@apply text-base-content;
		@apply font-bold text-sm;
	}

	th {
		@apply bg-transparent;
	}

	tbody tr:hover {
		@apply bg-primary/10;
	}
</style>
