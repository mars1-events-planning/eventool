<script lang="ts">
	import Trash from './icons/Trash.svelte';

	let guests = [
		{
			name: 'Иванов Петр',
			email: 'petr.ivanov.ivanovich20010@gmail.com',
			note: 'Друг жениха',
			tags: ['Веган', 'Сторона жениха', 'Еще что-то', 'Веган', 'Сторона жениха']
		},
		{
			name: 'Иван Петров',
			email: 'ivanpetrov@yandex.ru',
			note: 'Еще кто-то',
			tags: ['Сторона жениха']
		}
	];
</script>

<div
	class="w-full flex flex-col border border-primary rounded-lg"
>
	<div class="collapse collapse-arrow">
		<input type="checkbox" />
		<div class="collapse-title">
			<span class="font-semibold">Гости</span>
		</div>
		<div class="overflow-x-auto">
			<table class="table">
				<thead>
					<tr>
						<th>Гость</th>
						<th>Теги</th>
						<th>
							<button
								class="btn btn-primary btn-outline btn-sm flex flex-col items-center justify-center"
							>
								<span class="text-xl font-bold">+</span>
							</button>
						</th>
					</tr>
				</thead>
				<tbody>
					{#each guests as guest}
						<tr>
							<td class=" whitespace-nowrap">
								<div class="flex flex-col gap-2">
									<span class="text-md">{guest.name}</span>
									<span class="text-xs font-mono">{guest.email}</span>
								</div>
							</td>
							<td class="">
								<div class="flex flex-wrap gap-2">
									{#each guest.tags as tag}
										<span class="badge badge-outline badge-primary badge-sm">{tag}</span>
									{/each}
								</div>
							</td>
							<th>
								<button class="btn btn-error btn-outline btn-xs">
									<Trash size={20} />
								</button>
							</th>
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
	</div>
</div>
